using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrentHour : MonoBehaviour
{
    [SerializeField] private TMP_Text _hourText;
    [SerializeField] private Image _imageDay;
    [SerializeField] private List<Sprite> _dayIcon;
    [SerializeField] private GameObject _panelNoche;
    [SerializeField] private Sleep _sleepScript;
    [SerializeField] private PlayerData _playerData;

    void Start()
    {
        InvokeRepeating("ActualizarHora", 0.1f, 1f); // Llamo a la función ActualizarHora cada segundo

    }

    private void Update()
    {
        if (Time.timeScale <= 0)
        {
            ActualizarHora();
        }
    }

    void ActualizarHora()
    {
        // Obtengo la hora actual
        DateTime _currentHour = DateTime.Now;

        // Paso la hora a un formato de 24 horas
        int currentHour = _currentHour.Hour;
        int currentMinit = _currentHour.Minute;

        // Dependiendo de la hora le asigno un Sprite u otro
        if (currentHour >= 7 && (currentHour < 21 || (currentHour == 21 && currentMinit < 30)))
        {
            _imageDay.sprite = _dayIcon[0];
            if (!_sleepScript.IsResting())
            {
                _panelNoche.SetActive(false);
            }
        }
        else
        {
            _panelNoche.SetActive(true);
            _imageDay.sprite = _dayIcon[1];
            if (_playerData.EggHasBeenOpened())
            {
                _sleepScript.ForceSleep(Sleep.SleepReason.NightTime);
            }
        }

        string _formattedHour = _currentHour.ToString("HH:mm:ss");
        _hourText.text = _formattedHour;
    }
}