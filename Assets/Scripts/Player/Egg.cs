using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    //public event Action EggOpened; //Esto es un Evento (Una acci�n que se ejecuta y notifica a otro Script al que haga referencia)

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _rotationDuration = 2f;
    [SerializeField] private GameObject _player;
    [SerializeField] private Sleep _sleepBar;
    [SerializeField] private Hunger _hungerBar;
    [SerializeField] private Hapiness _hapinessBar;
    [SerializeField] private Button _sleepButton;
    [SerializeField] private Button _hungerButton;
    [SerializeField] private Button _minigamesButton;

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
        }
    }

    private void EggMovement()
    {
        Sequence sequence = DOTween.Sequence();

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
}