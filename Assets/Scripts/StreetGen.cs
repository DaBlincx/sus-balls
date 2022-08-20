using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

[ExecuteInEditMode]
public class StreetGen : MonoBehaviour
{
    public Settings Settings;
    public float Radius = 50;
    [Range(100, 300)]
    public int Subdivisions;

    private float lineSize;
    private int lineCount;

    public void OnValidate()
    {
        Start();
    }

    // Start is called before the first frame update
    void Start()
    {
        GC.Collect();
        //EditorUtility.UnloadUnusedAssetsImmediate();

        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        List<Vector2> uv = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();

        int indexOffset = 0;

        for (int i = 0; i < Subdivisions + 1; i++)
        {
            Vector3 mid = Quaternion.Euler(new Vector3((i / (float)Subdivisions) * 360.0f, 0, 0)) * Vector3.up * Radius;
            Vector3 p1 = mid - Vector3.right * (Settings.LaneWidth * Settings.LaneCount * 0.5f);
            Vector3 p2 = mid + Vector3.right * (Settings.LaneWidth * Settings.LaneCount * 0.5f);
            vertices.Add(p1);
            vertices.Add(p2);
            uv.Add(new Vector2(0, i / (float)Subdivisions));
            uv.Add(new Vector2(Settings.LaneCount, i / (float)Subdivisions));
            normals.Add(mid.normalized);
            normals.Add(mid.normalized);
        }

        for (int i = 0; i < Subdivisions; i++)
        {
            indices.Add((indexOffset + 1) );
            indices.Add((indexOffset));
            indices.Add((indexOffset + 3) );
            indices.Add((indexOffset + 2));
            indices.Add((indexOffset + 3));
            indices.Add((indexOffset));

            indexOffset += 2;
        }
        

        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();

        vertices.Clear();
        indices.Clear();
        normals.Clear();
        uv.Clear();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GC.Collect();
        //EditorUtility.UnloadUnusedAssetsImmediate();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineSize != Settings.LaneWidth)
        {
            lineSize = Settings.LaneWidth;
            Start();
        }
        if (lineCount != Settings.LaneCount)
        {
            lineCount = Settings.LaneCount;
            Start();
        }
    }
}
