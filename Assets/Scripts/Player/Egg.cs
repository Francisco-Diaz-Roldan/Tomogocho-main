using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class Egg : MonoBehaviour
{
    //public event Action EggOpened; //Esto es un Evento (Una acción que se ejecuta y notifica a otro Script al que haga referencia)

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _rotationDuration = 2f;
    [SerializeField] private GameObject _player;
    [SerializeField] private Sleep _sleepBar;
    [SerializeField] private Hunger _hungerBar;
    [SerializeField] private Hapiness _hapinessBar;
    [SerializeField] private Button _sleepButton;
    [SerializeField] private Button _hungerButton;
    [SerializeField] private Button _minigamesButton;
    [SerializeField] private TMP_Text _timeRemainingText;

    private bool isEgg = true;

    public bool IsEgg()
    {
        return isEgg;
    }

    private void Start()
    {
        if (_playerData.EggHasBeenOpened())
        {
            OpenEgg();
        }
        EggMovement();
    }

    void Update()
    {
        if (_playerData != null )
        {
            if ( _playerData.EggHasBeenOpened())
            {
                OpenEgg();
            }
            _playerData.LifeTimeInSeconds += Time.deltaTime;
            UpdateTimeRemainingText();
        }
    }

    private void EggMovement()
    {
        DG.Tweening.Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(0, 0, -25), _rotationDuration)); 
        sequence.Append(transform.DORotate(new Vector3(0, 0, 25), _rotationDuration)); 
        sequence.SetLoops(-1, LoopType.Yoyo); 
        sequence.Play();
    }


    private void OpenEgg() 
    {
        ActivateBars();
        ActivateButtons();
        _player.SetActive(true);
        DOTween.KillAll();
        gameObject.SetActive(false);
        isEgg = false;
    }

    private void ActivateBars()
    {
        _sleepBar.StartBar();
        _hungerBar.StartBar();
        _hapinessBar.StartBar();
    }

    private void ActivateButtons()
    {
        _sleepButton.interactable = true;
        _hungerButton.interactable = true;
        _minigamesButton.interactable = true;
    }

    private void UpdateTimeRemainingText()
    {
        float timeRemaining = _playerData.TimeToOpenEgg - _playerData.LifeTimeInSeconds;
        timeRemaining = Mathf.Max(timeRemaining, 0);  // Me aseguro de que no sea negativo

        if (timeRemaining <= 0)
        {
             _timeRemainingText.gameObject.SetActive(false);
        }
        else
        {
            // Convierto el tiempo restante a segundos enteros
            _timeRemainingText.text = $"Incubando huevo\r\nFaltan: {timeRemaining:N0} segundos para abrirse";
        }
    }
}
