using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    [SerializeField] private Image _sleepBar;
    [SerializeField] private GameObject _panelNoche;
    [SerializeField] private PlayerSleep _playerSleep;
    private bool isResting = false;
    private float restInterval = 1f;
    
    private void Start()
    {
        // Llama repetidamente al método que decrementa la barra de sueño con un intervalo de tiempo
        InvokeRepeating(nameof(DecreaseSleep), 1f, 1f);
    }

    // Método para decrementar la barra de sueño solo si no se está descansando
    private void DecreaseSleep()
    {
        if (!isResting) // Solo baja si no se está descansando
        {
            _sleepBar.fillAmount -= 0.01f; // Decrementa el porcentaje de sueño
            _sleepBar.fillAmount = Mathf.Max(_sleepBar.fillAmount, 0f); // Asegura que el valor no sea menor que 0
        }
    }

    // Método para iniciar o detener el descanso al pulsar un botón
    public void RestCreature()
    {
        isResting = !isResting; // Cambia el estado de descanso al presionar el botón

        if (isResting)
        {
            // Activar el GameObject Panel Noche al comenzar a descansar
            _panelNoche.SetActive(true);

            // Detiene la repetición del método que decrementa la barra de sueño
            CancelInvoke(nameof(DecreaseSleep));

            // Comienza a llamar repetidamente al método que incrementa la barra de sueño con un intervalo
            InvokeRepeating(nameof(IncreaseSleep), 0f, restInterval);

            // Aquí activamos la animación "Sleeping" en el Animator
            _playerSleep.ChangeSleepState(true);
        }
        else
        {
            // Desactivar el GameObject Panel Noche al dejar de descansar
            _panelNoche.SetActive(false);

            // Detiene la repetición del método que incrementa la barra de sueño
            CancelInvoke(nameof(IncreaseSleep));

            // Reinicia la repetición del método que decrementa la barra de sueño
            InvokeRepeating(nameof(DecreaseSleep), 1f, 1f);

            // Aquí desactivamos la animación "Sleeping" en el Animator
            _playerSleep.ChangeSleepState(false);
        }
    }

    // Método llamado repetidamente cuando se está descansando
    public void IncreaseSleep()
    {
        // Define la velocidad de incremento deseada
        float incrementSpeed = 0.025f;

        // Calcula el nuevo valor de fillAmount de manera más suave usando Lerp
        _sleepBar.fillAmount = Mathf.Lerp(_sleepBar.fillAmount, 1f, incrementSpeed);

        // Asegura que el valor no supere 1
        _sleepBar.fillAmount = Mathf.Min(_sleepBar.fillAmount, 1f);
    }

    // Método para detener el descanso cuando el jugador muere (opcional)
    private void HandlePlayerDeath()
    {
        // Detiene la repetición del método que incrementa la barra de sueño si el jugador muere
        CancelInvoke(nameof(IncreaseSleep));
    }

    public void SetPanelNoche(GameObject panel)
    {
        _panelNoche = panel;
    }
}