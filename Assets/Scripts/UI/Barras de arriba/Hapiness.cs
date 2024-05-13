using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Hapiness : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image _hapinessBar;
    public float HapinessBarPercent => _hapinessBar.fillAmount;
    private PlayerDead _playerDead;
    private PlayerSleep _playerSleep;
    public PlayerData playerData;
    #endregion

    private void Start()
    {
        _playerDead = FindObjectOfType<PlayerDead>();
        _playerSleep = FindObjectOfType<PlayerSleep>();
        InvokeRepeating(nameof(UpdateBar), 1f, 1f); // Llama repetidamente a UpdateBar con un intervalo de 1 segundo
    }

    // Aumenta el porcentaje de felicidad
    public void MakeFeelHappyCreature()
    {
        _hapinessBar.fillAmount = Mathf.Min(1f, _hapinessBar.fillAmount + .1f);
    }

    // Actualiza la barra de felicidad
    private void UpdateBar()
    {
        if (_playerDead != null && !_playerDead.IsDead)
        {
            if (_playerSleep != null && !_playerSleep.IsSleeping)
            {
                _hapinessBar.fillAmount -= 0.01f;
                _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

                if(playerData != null)
                {
                    playerData.HapinessPercent = _hapinessBar.fillAmount;
                }
            }
        }
    }
}
