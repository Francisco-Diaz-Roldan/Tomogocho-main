using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverData : MonoBehaviour
{
    [SerializeField] private TMP_Text _days;
    [SerializeField] private TMP_Text _hours;
    [SerializeField] private TMP_Text _minutes;
    [SerializeField] private PlayerData _playerData;

    private void OnEnable()
    {
        ConvertirSegundos(_playerData.LifeTimeInSeconds);
    }

    private void ConvertirSegundos(float segundos)
    {
        // 1 d�a equivale a 86400 segundos (24 horas * 60 minutos * 60 segundos)
        const float segundosEnUnDia = 86400;
        // 1 hora equivale a 3600 segundos (60 minutos * 60 segundos)
        const float segundosEnUnaHora = 3600;
        // 1 minuto equivale a 60 segundos
        const float segundosEnUnMinuto = 60;

        // Calcular d�as
        float dias = Mathf.Floor(segundos / segundosEnUnDia);
        // Calcular horas restantes despu�s de restar los d�as
        float horasRestantes = segundos % segundosEnUnDia;
        // Calcular horas
        float horas = Mathf.Floor(horasRestantes / segundosEnUnaHora);
        // Calcular minutos restantes despu�s de restar las horas
        float minutosRestantes = horasRestantes % segundosEnUnaHora;
        // Calcular minutos
        float minutos = Mathf.Floor(minutosRestantes / segundosEnUnMinuto);

        _days.text = $"D�as: {dias.ToString()}";
        _hours.text = $"Horas: {horas.ToString()}";
        _minutes.text = $"Minutos: {minutos.ToString()}";
    }

    public void Salir()
    {
        _playerData.ResetValues(false); //Creo un PLater Data vacio
        this.gameObject.SetActive(false);
    }
}