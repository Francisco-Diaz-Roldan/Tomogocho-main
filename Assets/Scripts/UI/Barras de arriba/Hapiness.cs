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

    private bool _lowSleepNotified = false;
    private bool _noSleepNotified = false;
    private bool _bitHungry = false;
    private bool _reallytHungry = false;

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
    /*private void UpdateBar()
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
                float decreaseAmount = 0.01f + (activePoopCount * 0.07f);

                _hapinessBar.fillAmount -= 0.01f;
                _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

                if(_playerData != null)
                {
                    _playerData.HapinessPercent = _hapinessBar.fillAmount;
                }
            }
        }
    }*/

    /* private void UpdateBar()
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
                 float decreaseAmount = 0.0075f + (activePoopCount * 0.005f);

                 _hapinessBar.fillAmount -= decreaseAmount;
                 _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

                 if (_playerData != null)
                 {
                     _playerData.HapinessPercent = _hapinessBar.fillAmount;
                 }
             }
         }

         // Reducción de la felicidad más rápido si se notifica bajo sueño o falta de sueño
         if (_lowSleepNotified && _playerSleep != null && !_playerSleep.IsSleeping)
         {
             // Reduce el valor de la barra de felicidad un 2.5% más rápido (además del valor de las cacas)
             float decreaseAmount = 0.01f * 1.0025f; // Incremento del 2.5% adicional
             _hapinessBar.fillAmount -= decreaseAmount;
             _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

             _lowSleepNotified = false;
         }

         if (_noSleepNotified && _playerSleep != null && !_playerSleep.IsSleeping)
         {
             // Reduce el valor de la barra de felicidad un 5% más rápido (además del valor de las cacas)
             float decreaseAmount = 0.01f * 1.005f; // Incremento del 5% adicional
             _hapinessBar.fillAmount -= decreaseAmount;
             _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

             _noSleepNotified = false;
         }

         // Manejo de notificaciones de hambre
         if (_bitHungry && _playerSleep != null && !_playerSleep.IsSleeping)
         {
             float decreaseAmount = 0.01f * 1.025f; 
             _hapinessBar.fillAmount -= decreaseAmount;
             _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

             _bitHungry = false;
         }

         if (_reallytHungry && _playerSleep != null && !_playerSleep.IsSleeping)
         {
             float decreaseAmount = 0.01f * 1.005f; 
             _hapinessBar.fillAmount -= decreaseAmount;
             _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

             _reallytHungry = false;
         }
     }*/

    private void UpdateBar()
    {
        if (!_eggIsOpened || _playerDead.IsDead)
        {
            return;
        }

        bool isSleeping = _playerSleep.IsSleeping;
        int activePoopCount = _playerPoop.GetActivePoopCount();

        float decreaseAmount = 0f;

        if (!isSleeping && !_playerDead.IsDead) //Aquí es donde baja la barra de vida
        {
            decreaseAmount = 0.005f + (activePoopCount * 0.005f);

            if (_bitHungry)
            {
                decreaseAmount *= 1.002f;
                _bitHungry = false;
            }
            else if (_reallytHungry)
            {
                decreaseAmount *= 1.005f;
                _reallytHungry = false;
            }

            if (_lowSleepNotified)
            {
                decreaseAmount *= 1.002f; 
                _lowSleepNotified = false;
            }
            else if (_noSleepNotified)
            {
                decreaseAmount *= 1.005f; 
                _noSleepNotified = false;
            }
        }

        _hapinessBar.fillAmount -= decreaseAmount;
        _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

        if (!_playerDead.IsDead && !isSleeping)
        {
            _playerData.HapinessPercent = _hapinessBar.fillAmount;
        }
    }

    public void NotifyLowSleep()
    {
        _lowSleepNotified = true;
    } 

    public void NotifyNoSleep()
    {
        _noSleepNotified = true;
    }

    public void NotifyBitHungry()
    {
        _bitHungry = true;
    }

    public void NotifyReallyHungry()
    {
        _reallytHungry = true;
    }
}
