using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Hapiness : MonoBehaviour
{
    [SerializeField] private Image _hapinessBar;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerPoop _playerPoop;
    private bool _eggIsOpened;
    public PlayerData _playerData;
    public float HapinessBarPercent => _hapinessBar.fillAmount;


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
                int activePoopCount = _playerPoop.GetActivePoopCount();
                float decreaseAmount = 0.01f + (activePoopCount * 0.05f);

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
