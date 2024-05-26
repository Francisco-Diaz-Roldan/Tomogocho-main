using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverData : MonoBehaviour
{
    [SerializeField] private TMP_Text _days;
    [SerializeField] private TMP_Text _hours;
    [SerializeField] private TMP_Text _minutes;
    [SerializeField] private PlayerData _playerData;

    [SerializeField] private bool _isGameOver = true;

    private void OnEnable()
    {
        if (_isGameOver)
        {
            ConvertirSegundos(_playerData.LifeTimeInSeconds);
        }
        else
        {
            ConvertirSegundos(_playerData.MostOldTomogochoTime);
        }
    }

    private void ConvertirSegundos(float segundos)
    {

        const float segundosEnUnDia = 86400; // (24 horas * 60 minutos * 60 segundos)
        const float segundosEnUnaHora = 3600; // (60 minutos * 60 segundos)
        const float segundosEnUnMinuto = 60;

        float dias = Mathf.Floor(segundos / segundosEnUnDia); // Calculo los días
        float horasRestantes = segundos % segundosEnUnDia; // Calculo las horas restantes después de restar los días
        float horas = Mathf.Floor(horasRestantes / segundosEnUnaHora);
        float minutosRestantes = horasRestantes % segundosEnUnaHora;
        float minutos = Mathf.Floor(minutosRestantes / segundosEnUnMinuto);

        _days.text = $"Días: {dias.ToString()}";
        _hours.text = $"Horas: {horas.ToString()}";
        _minutes.text = $"Minutos: {minutos.ToString()}";
    }

    public void Salir()
    {
        _playerData.ResetValues(false); //Creo un PLater Data vacio
        SceneManager.LoadScene("MainScene");
    }
}