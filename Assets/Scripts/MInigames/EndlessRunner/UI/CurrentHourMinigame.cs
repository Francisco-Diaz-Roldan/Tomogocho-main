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
        int currentMinute = _currentHour.Minute;

        // Dependiendo de la hora le asigno un Sprite u otro
        if (currentHour >= 7 && (currentHour < 21 || (currentHour == 21 && currentMinute < 30)))
        {
            _panelNoche.SetActive(false);
            _imageDay.sprite = _dayIcon[0];  // Asigno el sprite de la mañana
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

    // Agregar método para obtener la hora actual
    public int GetCurrentHour()
    {
        return DateTime.Now.Hour;
    }

    // Agregar método para obtener el minuto actual
    public int GetCurrentMinute()
    {
        return DateTime.Now.Minute;
    }
}