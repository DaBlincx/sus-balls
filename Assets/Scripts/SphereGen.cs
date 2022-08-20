using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]


[ExecuteInEditMode]
public class SphereGen : MonoBehaviour
{
    // Start is called before the first frame update
    public float Radius = 50;
    [Range(10, 100)]
    public int Subdivisions;
    public int FoliageCount = 100;


    private float oldRadius;
    private int oldSubdivisions, oldFoliageCount;

    public List<GameObject> FoliagePresets;
    

    private int currentVertexOffset = 0;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    struct PartMesh
    {
        public List<Vector3> vertices;
        public List<int> indices;

    }

    public void GenerateFoliage()
    {

        if (FoliageCount > 500)
            FoliageCount = 500;
        if (spawnedObjects == null)
            spawnedObjects = new List<GameObject>();
        else
            spawnedObjects.Clear();

        for (int i = 0; i < FoliageCount; i++)
        {
            
            Vector3 point = Random.onUnitSphere;

            float angle = Vector3.Angle(point, Vector3.right);
            if (angle > 80.0f && angle < 100.0f || angle < 40.0f || angle > 140.0f)
            {
                i--; // Wir versuchen es noch mal
                continue;
            }

            point = point * Radius;
            GameObject go = Instantiate(
                    FoliagePresets[Random.Range(0, FoliagePresets.Count)],
                    point,
                    Quaternion.FromToRotation(Vector3.up, point)
                );
            go.transform.parent = gameObject.transform;
            go.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            spawnedObjects.Add(go);
        }
    }

    public void OnValidate()
    {
         Generate();
    }

    PartMesh GeneratePlane(Vector3 a, Vector3 b, Vector3 c, Vector3 d, int subdivisions)
    {

        Vector3 stepsizeRow = (b - a) / (subdivisions + 1);
        Vector3 stepsizeCol = (c - a) / (subdivisions + 1);

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();

        // Create vertices
        for (int i = 0; i < subdivisions + 2; i++)
        {
            for (int j = 0; j < subdivisions + 2; j++)
            {
                vertices.Add(a + (j * stepsizeRow) + (i * stepsizeCol));
            }
        }
        // Connect vertices
        for (int i = 0; i < subdivisions + 1; i++)
        {
            for (int j = 0; j < subdivisions + 1; j++)
            {
                indices.Add(currentVertexOffset + (i + 1) + (j + 1) * (subdivisions + 2));
                indices.Add(currentVertexOffset + (i + 1) + j * (subdivisions + 2));
                indices.Add(currentVertexOffset + i + j * (subdivisions + 2));

                indices.Add(currentVertexOffset + i + j * (subdivisions + 2));
                indices.Add(currentVertexOffset + i + (j + 1) * (subdivisions + 2));
                indices.Add(currentVertexOffset + (i + 1) + (j + 1) * (subdivisions + 2));
            }
        }

        PartMesh outMesh = new PartMesh();
        outMesh.vertices = vertices;
        outMesh.indices = indices;

        return outMesh;
    }

    void Generate()
    {
        bool recalculateFoliage = true;
        foreach (GameObject go in spawnedObjects)
        {
            if (go != null)
            {
                recalculateFoliage = false;
            }
        }
        if ((spawnedObjects == null || recalculateFoliage) && gameObject.transform.childCount == 0)
        {
            GenerateFoliage();
        }

        GC.Collect();
        //EditorUtility.UnloadUnusedAssetsImmediate();
        Mesh mesh = new Mesh();
        currentVertexOffset = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        List<Vector2> uv = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();

        // Generate Cube Points
        Vector3 A = new Vector3(0, 0, 0);
        Vector3 B = new Vector3(0, 0, 1);
        Vector3 C = new Vector3(0, 1, 0);
        Vector3 D = new Vector3(0, 1, 1);
        Vector3 E = new Vector3(1, 0, 0);
        Vector3 F = new Vector3(1, 0, 1);
        Vector3 G = new Vector3(1, 1, 0);
        Vector3 H = new Vector3(1, 1, 1);

        PartMesh M1 = GeneratePlane(C, D, A, B, Subdivisions);
        currentVertexOffset += M1.vertices.Count;
        PartMesh M2 = GeneratePlane(H, G, F, E, Subdivisions);
        currentVertexOffset += M2.vertices.Count;
        PartMesh M3 = GeneratePlane(D, H, B, F, Subdivisions);
        currentVertexOffset += M3.vertices.Count;
        PartMesh M4 = GeneratePlane(G, C, E, A, Subdivisions);
        currentVertexOffset += M4.vertices.Count;
        PartMesh M5 = GeneratePlane(A, B, E, F, Subdivisions);
        currentVertexOffset += M5.vertices.Count;
        PartMesh M6 = GeneratePlane(G, H, C, D, Subdivisions);

        vertices.AddRange(M1.vertices);
        indices.AddRange(M1.indices);
        vertices.AddRange(M2.vertices);
        indices.AddRange(M2.indices);
        vertices.AddRange(M3.vertices);
        indices.AddRange(M3.indices);
        vertices.AddRange(M4.vertices);
        indices.AddRange(M4.indices);
        vertices.AddRange(M5.vertices);
        indices.AddRange(M5.indices);
        vertices.AddRange(M6.vertices);
        indices.AddRange(M6.indices);


        Vector3 Mid = new Vector3(0.5f, 0.5f, 0.5f);
        // Project on sphere
        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = (vertices[i] - Mid).normalized * Radius;
            normals.Add((vertices[i] - Mid).normalized);
        }


        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.normals = normals.ToArray();

        vertices.Clear();
        indices.Clear();
        normals.Clear();
        uv.Clear();
        if (this != null)
            GetComponent<MeshFilter>().sharedMesh = mesh;

        GC.Collect();
        //EditorUtility.UnloadUnusedAssetsImmediate();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
