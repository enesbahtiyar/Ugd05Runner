using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] road;
    [SerializeField] GameObject[] collectables;
    [SerializeField] Transform player;
    [SerializeField] Transform roadParent;

    float roadLength = 20;
    int startRoadCount = 2;

    private void Start()
    {
        Instantiate(road[0], transform.position, Quaternion.identity, roadParent);

        for (int i = 0; i < startRoadCount; i++)
        {
            GenerateRoad();
        }

        SpawnCollectable();
    }


    void SpawnCollectable()
    {
        GameObject collectableObject = Instantiate(collectables[Random.Range(0, collectables.Length)], player.position + new Vector3(0f, 0.5f, 50f), Quaternion.identity);

        Invoke("SpawnCollectable", Random.Range(3f, 10f));
    }

    private void Update()
    {
        if(player.position.z > roadLength / 2 - 20)
        {
            GenerateRoad();
        }
    }

    void GenerateRoad()
    {
        Instantiate(road[Random.Range(0, road.Length)], transform.position + new Vector3(0, 0, roadLength), Quaternion.identity, roadParent);
        roadLength += 20;
    }
}
