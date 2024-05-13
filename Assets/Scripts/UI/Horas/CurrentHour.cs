using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrentHour : MonoBehaviour
{
    public TMP_Text _hourText;
    public Image _imageDay;
    public List<Sprite> _dayIcon;

    void Start()
    {
        // Llamo a la función ActualizarHora cada segundo
        InvokeRepeating("ActualizarHora", 0f, 1f);
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
        // Consigo la hora actual del sistema
        DateTime _currentHour = DateTime.Now;

        // Paso la hora a un formato de 24 horas
        int currentHour = _currentHour.Hour;
        int currentMinit = _currentHour.Minute;

        // Dependiendo de la hora le asigno un Sprite u otro
        if (currentHour >= 9 && (currentHour < 20 || (currentHour == 20 && currentMinit < 30)))  // Entre las 6:00h y las 20:30h
        {
            _imageDay.sprite = _dayIcon[0];  // Asigno el sprite de la mañana
        }
        else
        {
            _imageDay.sprite = _dayIcon[1];  // Asigno el sprite de la tarde/noche
        }

        // Actualizo el texto de la hora en el componente TMP_Text
        string _formattedHour = _currentHour.ToString("HH:mm:ss");
        _hourText.text = _formattedHour;
    }
}