using System.Collections;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) { }
        Destroy(collision.gameObject);
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