using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject minotaur;
    [SerializeField] private GameObject orc;
    [SerializeField] private GameObject beholder;
    [SerializeField] private GameObject turtle;

    [SerializeField] private int countEnemySpawn;
    private float timeToSpawn;
    [SerializeField] private float spawnInterval = 2f;  
    private bool spawnStart;

    void Start()
    {
        timeToSpawn = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= timeToSpawn && countEnemySpawn > 0)
        {
            SpawnRandomEnemy();
            timeToSpawn = Time.time + spawnInterval; 
        }
    }

    void SpawnRandomEnemy()
    {
        GameObject[] enemies = new GameObject[] { minotaur, orc, beholder, turtle };
        System.Collections.Generic.List<GameObject> validEnemies = new System.Collections.Generic.List<GameObject>();

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                validEnemies.Add(enemy);
            }
        }

        if (validEnemies.Count > 0)
        {
            int randomIndex = Random.Range(0, validEnemies.Count);
            Instantiate(validEnemies[randomIndex], transform.position, Quaternion.identity);
            countEnemySpawn--;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        spawnStart = other.gameObject.layer == LayerMask.NameToLayer("Character");
    }
}
