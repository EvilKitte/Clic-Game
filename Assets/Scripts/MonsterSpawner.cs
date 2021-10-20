using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float xMinRange = -12.0f;
    public float xMaxRange = 12.0f;
    public float yMinRange = 3.0f;
    public float yMaxRange = 13.0f;
    public float zMinRange = -12.0f;
    public float zMaxRange = 12.0f;

    public bool canSpawn = false;

    public GameObject[] spawnObjects;

    private float nextSpawnTime;
    private float secondsBetweenSpawning = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + secondsBetweenSpawning;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnObjectAtRandomPlace();

                secondsBetweenSpawning = Random.Range(0.5f, 5f);
                nextSpawnTime = Time.time + secondsBetweenSpawning;
            }
        }
    }

    private void SpawnObjectAtRandomPlace()
    {
        Vector3 spawnPosition;
        spawnPosition.x = Random.Range(xMinRange, xMaxRange);
        spawnPosition.y = Random.Range(yMinRange, yMaxRange);
        spawnPosition.z = Random.Range(zMinRange, zMaxRange);

        int objectToSpawn = Random.Range(0, spawnObjects.Length - 1);

        if (!CheckCollision(spawnPosition, spawnObjects[objectToSpawn].transform.lossyScale))
        {
            GameObject spawnedObject = Instantiate(spawnObjects[objectToSpawn], spawnPosition, transform.rotation) as GameObject;
            spawnedObject.transform.parent = gameObject.transform;
        }
    }

    private bool CheckCollision(Vector3 centerPosition, Vector3 scale)
    {
        return Physics.BoxCast(centerPosition, scale / 2, new Vector3(0, 0, 0), Quaternion.identity, 0);
    }
}
