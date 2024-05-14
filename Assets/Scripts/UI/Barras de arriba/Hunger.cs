using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image _hungryBar;
    [SerializeField] private PlayerDead _playerDead; // Referencia al script PlayerDead
    [SerializeField] private PlayerSleep _playerSleep;
    public PlayerData _playerData;
    #endregion

    private void Start()
    {
        Debug.Log(_playerData == null);
        _hungryBar.fillAmount = _playerData.HungerPercent;
        
       // _playerDead = FindObjectOfType<PlayerDead>(); // Encuentra el componente PlayerDead en la escena
      //  _playerSleep = FindObjectOfType<PlayerSleep>();
        if (_playerDead == null || _playerSleep == null) { return; }

        // Llama repetidamente a UpdateBar con un intervalo de 1 segundo si el jugador no está muerto
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping) { InvokeRepeating(nameof(UpdateBar), 1f, 1f); }
    }

    // Alimenta a la criatura aumentando el porcentaje de hambre
    public void FeedCreature()
    {
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping) { _hungryBar.fillAmount = Mathf.Min(1f, _hungryBar.fillAmount + .1f); }
    }

    // Actualiza la barra de hambre
    private void UpdateBar()
    {
        // Disminuye el porcentaje de hambre solo si el jugador no está muerto
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping)
        {
            _hungryBar.fillAmount -= .01f;
            _hungryBar.fillAmount = Mathf.Max(_hungryBar.fillAmount, 0f);

            // Guardo el valor actual de _hungryBar.fillAmount en HungerPercent de PlayerData
            if (_playerData != null)
            {
                _playerData.HungerPercent = _hungryBar.fillAmount;
            }
        }
    }
}