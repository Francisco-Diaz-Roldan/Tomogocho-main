using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/SaveData")]

public class PlayerData : ScriptableObject
{

    public float HungerPercent;
    public float HapinessPercent;
    public float SleepPercent;
    public DateTime? StartLifeTime; //TODO para futura mejora que incluya la fecha en la que empecé la partida
    public float LifeTimeInSeconds;   //Para medir la cantidad de tiempo jugado
    [SerializeField] private float TimeToOpenEgg = 60f;

    public void ResetValues(bool newGame = true)
    {
        if (newGame)
        {
            HungerPercent = 1f;
            HapinessPercent = 1f;
            SleepPercent = 1f;
            StartLifeTime = DateTime.Now;
        }
        else
        {
            HungerPercent = 0f;
            HapinessPercent = 0f;
            SleepPercent = 0f;
            StartLifeTime = null;
        }
        LifeTimeInSeconds = 0f;
    }

    public bool HasDied()
    {
        return HapinessPercent == 0;
    }

    public bool EggHasBeenOpened()
    {
        return LifeTimeInSeconds >= TimeToOpenEgg;
    }
}
