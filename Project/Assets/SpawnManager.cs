using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject GoalPoint;
    public GameObject prefab;  // The prefab object to spawn
    public int numberOfObjects = 20;  // Number of objects to spawn
    public float spawnRange = 20.0f;  // Spawn range on x and z axis
    public float spawnHeight = 0.0f;  // Spawn height on y axis

    private Node[] nodeArray;  // Array to store Node components of spawned objects
    private List<Node> nodeList;
    private List<GameObject> ObjectList;

    void Awake()
    {
        nodeList = new List<Node>(numberOfObjects);
        ObjectList = new List<GameObject>(numberOfObjects);
        nodeArray = new Node[numberOfObjects];

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Randomly generate spawn position within spawn range
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosY = spawnHeight;
            float spawnPosZ = Random.Range(-spawnRange, spawnRange);

            // Spawn object at the generated position and rotation
            GameObject obj = Instantiate(prefab, new Vector3(spawnPosX, spawnPosY, spawnPosZ), Quaternion.identity);
            ObjectList.Add(obj);
            // Get Node component of spawned object and set DangerLevel attribute
            Node node = obj.GetComponent<Node>();
            if (node != null)
            {
                node.DangerLevel = GetRandomDangerLevel();
                nodeArray[i] = node;
            }

            // Set ConnectsTo attribute by randomly selecting half of the nodeArray
        }
        
        for (int i = 0; i < nodeArray.Length; i++)
        {
            if (Random.Range(0, 4) == 0)  // Randomly choose whether to add connection
            {
                nodeList.Add(nodeArray[i]);
            }
        }
        StartPoint.GetComponent<Node>().ConnectsTo = nodeList.ToArray();
        nodeList.Clear();

        for (int i = 0; i < numberOfObjects; i++)
        {
            for (int j = 0; j < nodeArray.Length; j++)
            {
                if (Random.Range(0, 2) == 0)  // Randomly choose whether to add connection
                {

                    nodeList.Add(nodeArray[j]);
                }
            }
            if (Random.Range(0, 2) == 0)
            {
                nodeList.Add(GoalPoint.GetComponent<Node>());
            }
            ObjectList[i].GetComponent<Node>().ConnectsTo = nodeList.ToArray();
            nodeList.Clear();
        }
    }

    // Helper function to generate random danger level (more likely to be 0)
    private float GetRandomDangerLevel()
    {
        float randomValue = Random.Range(0.0f, 1.0f);
        if (randomValue < 0.5f)
        {
            return 0.0f;
        }
        else
        {
            return Random.Range(0.0f, 10.0f);
        }
    }
}
