/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] _obstacles;
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, _obstacles.Length);
            float minTime = 0.6f;
            float maxTime = 1.8f;
            float randomTime = Random.Range(minTime, maxTime);
            Instantiate(_obstacles[randomIndex], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }
}*/

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

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    private List<GameObject> obstaclePool = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            float minTime = 0.6f;
            float maxTime = 1.8f;
            float randomTime = Random.Range(minTime, maxTime);

            // Obtener un obstáculo de la piscina
            GameObject obstacle = GetPooledObstacle();
            obstacle.transform.position = transform.position;

            yield return new WaitForSeconds(randomTime);
        }
    }

    private GameObject GetPooledObstacle()
    {
        // Buscar un obstáculo inactivo en el pool
        foreach (GameObject obstacle in obstaclePool)
        {
            if (!obstacle.activeInHierarchy)
            {
                obstacle.SetActive(true);
                return obstacle;
            }
        }

        // Si no se encuentra un obstáculo inactivo, crear uno nuevo y lo añade al pool
        GameObject newObstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        obstaclePool.Add(newObstacle);
        return newObstacle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Desactiva el obstáculo al colisionar con otro objeto
        //collision.gameObject.SetActive(false);
    }
}

}*/