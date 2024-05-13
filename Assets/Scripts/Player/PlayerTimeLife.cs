using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeLife : MonoBehaviour
{

    public PlayerData playerData; 
    private float lifeTime; 

    void Start()
    {
        lifeTime = 0f;
    }

    void Update()
    {
        if (playerData != null) {
            lifeTime += Time.deltaTime;  // Aumento el tiempo  de vida de la criatura con el tiempo transcurrido en el último frame
            playerData.LifeTimeInSeconds = lifeTime; 
        }
    }
}
