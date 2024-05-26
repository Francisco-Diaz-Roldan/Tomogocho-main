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
    public float LifeTimeInSeconds;   //Para medir la cantidad de tiempo jugado
    public float TimeToOpenEgg = 60f;
    public float MostOldTomogochoTime = 0f;

    public void ResetValues(bool newGame = true)
    {
        if (newGame)
        {
            HungerPercent = 1f;
            HapinessPercent = 1f;
            SleepPercent = 1f;
        }
        else
        {
            HungerPercent = 0f;
            HapinessPercent = 0f;
            SleepPercent = 0f;
        }
        LifeTimeInSeconds = 0f;
        TimeToOpenEgg = 40f;   //Establezco el tiempo por defecto apra que se abra el huevo
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