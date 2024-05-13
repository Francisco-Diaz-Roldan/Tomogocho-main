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


    //Cuando le de a nueva partida hacer un PLayerData.ResetValues() porque necesita una referencia previa (Hacer otro script)
    //Cuando le dé a jugar y Seconds = 0f; -> Hacer un ResetValues() para
    //Los datos siempre están cargados -> CUando inicie la escena tengo que coger los percents y tengo que ver cómo guardar los segundos -> Hacer un script que sea un contador de tiempo que debe 
    //Necesito los porcentajes cuando inicio partida para que cada barra se cargue, cuando guarde partida que vuelque los datos al playerData
    //Necesito un script de un invokerepeating de cada segundo en el cual se incremente un segundo por cada segundo en el temporizador que lo coja del PLayerDataSeconds +=seconds;

}
