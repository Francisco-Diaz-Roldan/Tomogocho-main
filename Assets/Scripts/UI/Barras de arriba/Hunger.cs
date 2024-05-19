using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image _hungryBar;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerEating _playerEating;
    public PlayerData _playerData;
    private bool _eggIsOpened;

    #endregion

    private void Start()
    {
        _hungryBar.fillAmount = _playerData.HungerPercent;
        if (_playerDead == null || _playerSleep == null) { return; }

        // Llama repetidamente a UpdateBar con un intervalo de 1 segundo si el jugador no está muerto
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping) {
            InvokeRepeating(nameof(UpdateBar), 1f, 1f);
        }
    }

    public void StartBar()
    {
        _eggIsOpened = true;
    }

    // Se da de comer a la criatura aumentando el porcentaje de la barra de hambre
    public void FeedCreature()
    {
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping) 
        {
            _playerEating.ActivateEatingFace(true);
            _hungryBar.fillAmount = Mathf.Min(1f, _hungryBar.fillAmount + .1f);
            StartCoroutine(DisactivateEatingCoroutine());
        }
    }

    // Corrutina para desactivar la cara de comer después de un tiempo
    private IEnumerator DisactivateEatingCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _playerEating.ActivateEatingFace(false);
    }


    // Actualiza la barra de hambre
    private void UpdateBar()
    {
        if (!_eggIsOpened) 
        {
            return;
        }

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