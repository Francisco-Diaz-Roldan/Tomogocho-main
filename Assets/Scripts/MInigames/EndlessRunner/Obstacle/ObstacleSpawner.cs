using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private int poolSize = 10; // Tamaño del pool
    private List<GameObject> obstaclePool = new List<GameObject>();

    void Start()
    {
        InitializePool();
        StartCoroutine(SpawnObstacle());
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            foreach (GameObject obstaclePrefab in _obstacles)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
                obstacle.SetActive(false);
                obstaclePool.Add(obstacle);
            }
        }
    }

    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            float minTime = 0.6f;
            float maxTime = 1.8f;
            float randomTime = Random.Range(minTime, maxTime);

            // Obtener un obstáculo del pool
            GameObject obstacle = GetPooledObstacle();
            if (obstacle != null)
            {
                obstacle.transform.position = transform.position;
                obstacle.SetActive(true);
            }

            yield return new WaitForSeconds(randomTime);
        }
    }

    private GameObject GetPooledObstacle()
    {
        foreach (GameObject obstacle in obstaclePool)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }
        return null; // Retorna null si no hay objetos disponibles en el pool
    }
}
