using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Image _hungryBar;
    //private Coroutine _coroutine;
    #endregion

    private void Start()
    {
        // Para pausar el juego -> Time.timeScale = 0f; poner a 1 para reanudar

        // Llama repetidamente a UpdateBar con un intervalo de 1 segundo
        InvokeRepeating(nameof(UpdateBar), 1f, 1f);
    }

    // Alimenta a la criatura aumentando el porcentaje de hambre
    public void FeedCreature()
    {
       _hungryBar.fillAmount = Mathf.Min(1f, _hungryBar.fillAmount + .1f);
    }

    // Actualiza la barra de hambre
    private void UpdateBar()
    {
        _hungryBar.fillAmount -= .01f;
        _hungryBar.fillAmount = Mathf.Max(_hungryBar.fillAmount, 0f);
    }

    // TODO: Adaptar el tiempo en el que se va bajando la barra y cómo ésta se va llenando
    // TODO: Ver cómo hacer que el tiempo siga transcurriendo aunque la aplicación esté cerrada
    // TODO: Hacer la lógica para almacenar los valores de la barra

    private void PlayerDead(){
    //Que se instancie un sprite de una lapida y que suene una musiquita de morición
    }
}