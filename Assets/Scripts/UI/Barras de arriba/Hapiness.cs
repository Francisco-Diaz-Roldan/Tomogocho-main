using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;



public class Hapiness : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image _hapinessBar;
    public float HapinessBarPercent => _hapinessBar.fillAmount;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerSleep _playerSleep;
    public PlayerData _playerData;
    private bool _eggIsOpened;

    #endregion

    private void Start()
    {
        _hapinessBar.fillAmount = _playerData.HapinessPercent;
        InvokeRepeating(nameof(UpdateBar), 1f, 1f); // Llama repetidamente a UpdateBar con un intervalo de 1 segundo
    }

    public void StartBar()
    {
        _eggIsOpened = true;
    }

    public void MakeFeelHappyCreature() // Aumenta el porcentaje de felicidad
    {
        _hapinessBar.fillAmount = Mathf.Min(1f, _hapinessBar.fillAmount + .1f);
    }

    // Actualiza la barra de felicidad
    private void UpdateBar()
    {
        if (!_eggIsOpened)
        {
            return;
        }

        if (_playerDead != null && !_playerDead.IsDead)
        {
            if (_playerSleep != null && !_playerSleep.IsSleeping)
            {
                _hapinessBar.fillAmount -= 0.01f;
                _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

                if(_playerData != null)
                {
                    _playerData.HapinessPercent = _hapinessBar.fillAmount;
                }
            }
        }
    }
}
