using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeLife : MonoBehaviour
{
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerData _playerData;

    void Update()
    {
        if (_playerData != null && (_playerDead != null && !_playerDead.IsDead)) {
            // lifeTime += Time.deltaTime;  // Aumento el tiempo  de vida de la criatura con el tiempo transcurrido en el último frame
            _playerData.LifeTimeInSeconds += Time.deltaTime;
            Debug.Log("Tiempo de vida de la criatura: " + _playerData.LifeTimeInSeconds.ToString("F2") + " segundos");
        }
    }
}