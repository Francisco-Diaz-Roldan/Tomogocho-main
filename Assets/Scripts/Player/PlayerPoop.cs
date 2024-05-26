using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoop : MonoBehaviour
{
    [SerializeField] Image _hungerPercent;
    [SerializeField] GameObject _pooPrefab;
    [SerializeField] float _poopHungerTime;
    [SerializeField] float _poopNohungerTime;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerDead _playerDead;

    private List<GameObject> pooledObjects = new List<GameObject>();

    private float _poopTime = 0;
    private int poolSize = 10;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_pooPrefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    void Update()
    {
        {
            if (_playerSleep.IsSleeping || (_playerDead != null && _playerDead.IsDead))
            {
                return;
            }
            CheckPoop();
        }
    }

    private void CheckPoop()
    {
        _poopTime += Time.deltaTime;

        float currentHungerPercent = _hungerPercent.fillAmount;

        if (currentHungerPercent >= 0.5f && _poopTime >= _poopNohungerTime)
        {
            InstantiatePoop();
        }
        else if (currentHungerPercent < 0.5f && _poopTime >= _poopHungerTime)
        {
            InstantiatePoop();
        }
    }

    private void InstantiatePoop()
    {
        _poopTime = 0f;

        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = transform.position;
                obj.SetActive(true);
                return;
            }
        }
    }

    public int GetActivePoopCount()
    {
        int activeCount = 0;
        foreach (GameObject obj in pooledObjects)
        {
            if (obj.activeInHierarchy)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
}