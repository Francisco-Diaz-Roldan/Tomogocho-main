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

    void ActualizarHora()
    {
        // Obtener la hora actual del sistema
        DateTime _currentHour = DateTime.Now;

        // Obtener la hora en formato de 24 horas
        int currentHour = _currentHour.Hour;
        int currentMinit = _currentHour.Minute;

        //Debug.Log($"{currentHour}: {currentMinit}");

        // Determinar qué sprite asignar según la hora actual
        if (currentHour >= 9 && (currentHour < 20 || (currentHour == 20 && currentMinit < 30)))  // Entre las 6:00h y las 20:30h
        {
            _imageDay.sprite = _dayIcon[0];  // Asignar el sprite de la mañana
        }
        else
        {
            _imageDay.sprite = _dayIcon[1];  // Asignar el sprite de la tarde/noche
        }

        // Actualizar el texto de la hora en el componente TMP_Text
        string _formattedHour = _currentHour.ToString("HH:mm:ss");
        _hourText.text = _formattedHour;
    }
}