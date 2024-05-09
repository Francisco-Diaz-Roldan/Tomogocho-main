using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoop : MonoBehaviour
{
    [SerializeField]
    Image _hungerPercent;
    [SerializeField]
    GameObject _pooPrefab; // Prefab del objeto que quieres instanciar (Poo)
    [SerializeField]
    float _poopHungerTime;
    [SerializeField]
    float _poopNohungerTime;

    private float _poopTime = 0;

    private PlayerSleep _playerSleep;
    private PlayerDead _playerDead;


    private List<GameObject> pooledObjects = new List<GameObject>();
    private int poolSize = 5; // Tama�o del pool (cantidad m�xima de objetos Poo que pueden existir a la vez)

    // Start is called before the first frame update
    void Start()
    {
        _playerSleep = GetComponent<PlayerSleep>();
        _playerDead = GetComponent<PlayerDead>();
        if (_playerDead == null)
        {
            Debug.LogError("El componente PlayerDead no est� asignado correctamente en el script PlayerPoop.");
        }

        // Inicializar el pool al comienzo del juego
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_pooPrefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (_playerSleep.IsSleeping || (_playerDead != null && !_playerDead.IsDead)) return;

        CheckPoop();
    }*/
    void Update()
    {
        if (_playerSleep.IsSleeping)
        {
            Debug.Log("El jugador est� dormido. No se generar�n caquitas");
            return;
        }

        if (_playerDead != null)
            if (_playerDead.IsDead)
            {
                Debug.Log("El jugador est� muerto. No se generar�n caquitas");
                return;
            }
        else
        {
            Debug.Log("El jugador est� vivo. Se generar�n caquitas");
        }

        CheckPoop();
    }

    private void CheckPoop()
    {
        _poopTime += Time.deltaTime;
        if (_hungerPercent.fillAmount >= 0.5f)
        {
            if (_poopTime >= _poopNohungerTime)
            {
                InstantiatePoop();
            }
        }
        else
        {
            if (_poopTime >= _poopHungerTime)
            {
                InstantiatePoop();
            }
        }
    }

    private void InstantiatePoop()
    {
        _poopTime = 0f;

        // Buscar un objeto inactivo en el pool para reutilizar
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = transform.position;
                obj.SetActive(true);
                return;
            }
        }

        // Si todos los objetos en el pool est�n activos, no crear m�s poos
        Debug.LogWarning("Ya est� el m�ximo de cacas. No se pueden crear m�s en este momento.");
    }
}