using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrentHourMinigame : MonoBehaviour
{
    [SerializeField] private TMP_Text _hourText;
    [SerializeField] private Image _imageDay;
    [SerializeField] private List<Sprite> _dayIcon;
    [SerializeField] private GameObject _panelNoche;

    void Start()
    {
        // Llamo a la funci�n ActualizarHora cada segundo
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
        int currentMinute = _currentHour.Minute;

        // Dependiendo de la hora le asigno un Sprite u otro
        if (currentHour >= 8 && (currentHour < 21 || (currentHour == 21 && currentMinute < 30)))  // Entre las 8:00h y las 20:30h
        //if ( (currentHour >8  && currentMinute < 30))
        {
            _panelNoche.SetActive(false);
            _imageDay.sprite = _dayIcon[0];  // Asigno el sprite de la ma�ana
        }
        else
        {
            _panelNoche.SetActive(true);
            _imageDay.sprite = _dayIcon[1];  // Asigno el sprite de la tarde/noche
        }

        // Actualizo el texto de la hora en el componente TMP_Text
        string _formattedHour = _currentHour.ToString("HH:mm:ss");
        _hourText.text = _formattedHour;
    }

    // Agregar m�todo para obtener la hora actual
    public int GetCurrentHour()
    {
        return DateTime.Now.Hour;
    }

    // Agregar m�todo para obtener el minuto actual
    public int GetCurrentMinute()
    {
        return DateTime.Now.Minute;
    }
}