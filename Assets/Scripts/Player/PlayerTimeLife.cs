using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeLife : MonoBehaviour
{
    private PlayerDead _playerDead;
    public PlayerData playerData; 

    void Update()
    {
        if (playerData != null && (_playerDead != null && !_playerDead.IsDead)) {
           // lifeTime += Time.deltaTime;  // Aumento el tiempo  de vida de la criatura con el tiempo transcurrido en el �ltimo frame
            playerData.LifeTimeInSeconds += Time.deltaTime;
            Debug.Log("Tiempo de vida de la criatura: " + playerData.LifeTimeInSeconds.ToString("F2") + " segundos");
        }
    }
}