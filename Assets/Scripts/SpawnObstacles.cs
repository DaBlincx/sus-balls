using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public Settings Settings;
    public GameObject Ship; // We need the ship position
    public PlayerStats Player;
    public GameStateHandler GameState;

    public List<GameObject> Obstacles = new List<GameObject>();
    public List<GameObject> PowerUps = new List<GameObject>();

    private Vector3 spawnPosition;
    private float distanceToLastSpawn; // Distanz zum letzten gespawnten Objekt
    private Vector3 lastObjectPosition;

    private Queue<GameObject> spawnedObjects;

    void Start()
    {
        spawnedObjects = new Queue<GameObject>();
        lastObjectPosition = -(Ship.transform.position);
    }
 
    void SpawnObject()
    {

        int id = Random.Range(0, Obstacles.Count);

        // Count of possible positions
        int lanePos0 = Random.Range(0, Settings.LaneCount - Obstacles[id].GetComponent<PositionAccess>().Width + 1);

        // Actual lane position
        int lanePos = lanePos0 - Settings.LaneCount/2;

        Vector3 pos = spawnPosition + (Vector3.right * Settings.LaneWidth * lanePos);
        int laneHeight = 0;
        if (Obstacles[id].GetComponent<PositionAccess>().allowFly)
        {
            laneHeight = Random.Range(0, 4 - Obstacles[id].GetComponent<PositionAccess>().Height);
            pos += spawnPosition.normalized * Settings.FloorHeight * laneHeight;
        }

        GameObject obstacle = Instantiate(
                Obstacles[id],
                pos,
                Ship.transform.rotation * Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f))
            );
        obstacle.transform.localScale = Vector3.Scale(
            obstacle.transform.localScale,
            new Vector3(Settings.LaneWidth, Settings.FloorHeight, Settings.LaneWidth)
            );
        spawnedObjects.Enqueue(obstacle);
        
        if (Random.Range(0,100.0f) <= Settings.PowerUpChance)
        {
            if (obstacle.GetComponent<PositionAccess>().PowerUpPositions.Count > 0)
                SpawnPowerUpOnObstacle(obstacle, lanePos0, laneHeight);
        }

    }

    void SpawnPowerUpOnObstacle(GameObject obstacle, int posX, int posY)
    {
        int id = Random.Range(0, PowerUps.Count);

        // Count of possible positions
        List<Vector2Int> listOfPositions = obstacle.GetComponent<PositionAccess>().PowerUpPositions;
        int lanePos = (posX + listOfPositions[Random.Range(0, listOfPositions.Count)].x) % Settings.LaneCount;
        Debug.Log(lanePos);
        int laneHeight = (posY + listOfPositions[Random.Range(0, listOfPositions.Count)].y) % 3;

        // Actual lane position
        lanePos = lanePos - Settings.LaneCount / 2;
        Vector3 pos = spawnPosition + (Vector3.right * Settings.LaneWidth * lanePos);   
        pos += spawnPosition.normalized * Settings.FloorHeight * laneHeight;

        GameObject powerUp = Instantiate(
                PowerUps[id],
                pos,
                Ship.transform.rotation * Quaternion.Euler(new Vector3(180.0f, 0.0f, 0.0f))
            );
        powerUp.transform.localScale = Vector3.Scale(
            powerUp.transform.localScale,
            new Vector3(Settings.LaneWidth, Settings.FloorHeight, Settings.LaneWidth)
            );
        spawnedObjects.Enqueue(powerUp);
    }


    void Update()
    {
        Debug.Log(GameState.isIngame);
        if (GameState.isIngame == true)
        {
            spawnPosition = -(Ship.transform.position);
            distanceToLastSpawn = Vector3.Angle(lastObjectPosition, spawnPosition);

            if (distanceToLastSpawn >= Settings.GapSize)
            {
                lastObjectPosition = -(Ship.transform.position);

                SpawnObject();
            }
            CheckAndDelete();
        }
    }

    void CheckAndDelete()
    {
        if (spawnedObjects.Count > 0)
        {
            GameObject toTest = spawnedObjects.Peek();
            if (Vector3.Angle(toTest.transform.position, Ship.transform.position) > 10.0f &&
                Vector3.Dot(toTest.transform.position - Ship.transform.position, Ship.transform.forward) < 0) 
            {
                if (toTest.GetComponent<PositionAccess>().type == PositionAccess.Type.Damage)
                    Player.Punkte++;
                spawnedObjects.Dequeue();
                if (toTest != null)
                    Destroy(toTest);
                
            }
        }
    }

    public void ResetObstacles()
    {
        while (spawnedObjects.Count > 0)
        {
            GameObject toTest = spawnedObjects.Peek();
            if (toTest != null)
                Destroy(toTest);
            spawnedObjects.Dequeue();
        }
    }

}
