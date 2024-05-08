using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Hapiness : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Image _hapinessBar;
    public float HapinessBarPercent => _hapinessBar.fillAmount;
    #endregion

    private void Start()
    {
        // Para pausar el juego -> Time.timeScale = 0f; poner a 1 para reanudar

        // Llama repetidamente a UpdateBar con un intervalo de 1 segundo
        InvokeRepeating(nameof(UpdateBar), 1f, 1f);
    }

    // Aumenta el porcentaje de felicidad
    public void MakeFeelHappyCreature()
    {
        _hapinessBar.fillAmount = Mathf.Min(1f, _hapinessBar.fillAmount + .1f);
    }

    // Actualiza la barra de felicidad
    private void UpdateBar()
    {
        _hapinessBar.fillAmount -= .01f;
        _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);
    }

    private void PlayerDead()
    {

    }
}
