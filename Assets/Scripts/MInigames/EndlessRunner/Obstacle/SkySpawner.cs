using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _dayObstacles;
    [SerializeField] private GameObject[] _nightObstacles;
    [SerializeField] private int poolSize = 10;
    private CurrentHourMinigame currentHourMinigame; // Referencia al script CurrentHourMinigame
    private List<GameObject> obstaclePool = new List<GameObject>();
    private Coroutine spawnCoroutine;

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        currentHourMinigame = GetComponent<CurrentHourMinigame>();
        if (currentHourMinigame == null)
        {
            currentHourMinigame = FindObjectOfType<CurrentHourMinigame>();

            // Si sigue siendo null, mostramos un mensaje de error
            if (currentHourMinigame == null)
            {
                Debug.LogError("El componente CurrentHourMinigame no se ha asignado correctamente.");
                return;
            }
        }
            // Inicializar el pool de obstáculos según la hora actual
            UpdateObstaclePool();
    }

    private void UpdateObstaclePool()
    {
        if (currentHourMinigame == null) { return; }

        // Obtener la hora actual del script CurrentHourMinigame
        int currentHour = currentHourMinigame.GetCurrentHour();
        int currentMinute = currentHourMinigame.GetCurrentMinute();

        // Seleccionar el array de obstáculos adecuado según la hora
        GameObject[] obstaclesToSpawn = (currentHour >= 8 && (currentHour < 21 || (currentHour == 21 && currentMinute < 30))) ? _dayObstacles : _nightObstacles;
        //GameObject[] obstaclesToSpawn = (currentHour < 8 && currentMinute < 30) ? _dayObstacles : _nightObstacles;

        // Inicializar el pool de obstáculos según el array seleccionado
        InitializePoolFromArray(obstaclesToSpawn);
    }

    private void InitializePoolFromArray(GameObject[] obstacles)
    {
        // Limpiar el pool actual
        obstaclePool.Clear();

        // Llenar el pool con los obstáculos del array seleccionado
        foreach (GameObject obstaclePrefab in obstacles)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
                obstacle.SetActive(false);
                obstaclePool.Add(obstacle);
            }
        }
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnObstacle());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            float minTime = 0.6f;
            float maxTime = 1.8f;
            float randomTime = Random.Range(minTime, maxTime);

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
        return null;
    }
}
