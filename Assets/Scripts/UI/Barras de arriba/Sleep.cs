using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    [SerializeField] private Image _sleepBar;
    [SerializeField] private GameObject _panelNoche;
    [SerializeField] private PlayerSleep _playerSleep;
    private PlayerHappy _playerHappy;
    private PlayerDead _playerDead;

    private bool isResting = false;
    private float restInterval = 1f;

    private void Awake()
    {
        // Obtengo la referencia al componente PlayerHappy
        _playerHappy = FindObjectOfType<PlayerHappy>();
    }

    private void Start()
    {
        _playerDead = FindObjectOfType<PlayerDead>();
        if (_playerDead == null) { return; }
        // Llama repetidamente al m�todo que decrementa la barra de sue�o con un intervalo de tiempo
        if (!_playerDead.IsDead) { InvokeRepeating(nameof(DecreaseSleep), 1f, 1f); }
    }

    // M�todo para decrementar la barra de sue�o solo si no se est� descansando
    private void DecreaseSleep()
    {
        if (!isResting && !_playerDead.IsDead) // Solo baja si no se est� descansando
        {
            _sleepBar.fillAmount -= 0.01f; // Decrementa el porcentaje de sue�o
            _sleepBar.fillAmount = Mathf.Max(_sleepBar.fillAmount, 0f); // Asegura que el valor no sea menor que 0
        }
    }

    // M�todo para iniciar o detener el descanso al pulsar un bot�n
    public void RestCreature()
    {
        isResting = !isResting; // Cambia el estado de descanso al presionar el bot�n

        if (isResting)
        {
            // Activar el GameObject Panel Noche al comenzar a descansar
            _panelNoche.SetActive(true);

            // Detiene la repetici�n del m�todo que decrementa la barra de sue�o
            CancelInvoke(nameof(DecreaseSleep));

            // Comienza a llamar repetidamente al m�todo que incrementa la barra de sue�o con un intervalo
            InvokeRepeating(nameof(IncreaseSleep), 0f, restInterval);

            // Desactivo la animaci�n "Sleeping" en el Animator
            _playerSleep.ChangeSleepState(true);

            // Desactivo la cara feliz del jugador
            _playerHappy.ActivateHappyFace(false);
        }
        else
        {
            // Desactivar el GameObject Panel Noche al dejar de descansar
            _panelNoche.SetActive(false);

            // Detiene la repetici�n del m�todo que incrementa la barra de sue�o
            CancelInvoke(nameof(IncreaseSleep));

            // Reinicia la repetici�n del m�todo que decrementa la barra de sue�o
            InvokeRepeating(nameof(DecreaseSleep), 1f, 1f);

            // Aqu� desactivamos la animaci�n "Sleeping" en el Animator
            _playerSleep.ChangeSleepState(false);
        }
    }

    // M�todo llamado repetidamente cuando se est� descansando
    public void IncreaseSleep()
    {
        if (!_playerDead.IsDead)
        {
            // Define la velocidad de incremento deseada
            float incrementSpeed = 0.025f;

        // Calcula el nuevo valor de fillAmount de manera m�s suave usando Lerp
        _sleepBar.fillAmount = Mathf.Lerp(_sleepBar.fillAmount, 1f, incrementSpeed);

        // Asegura que el valor no supere 1
        _sleepBar.fillAmount = Mathf.Min(_sleepBar.fillAmount, 1f);
        }
    }

    // M�todo para detener el descanso cuando el jugador muere (opcional)
    private void HandlePlayerDeath()
    {
        // Detiene la repetici�n del m�todo que incrementa la barra de sue�o si el jugador muere
        CancelInvoke(nameof(IncreaseSleep));
    }

    public void SetPanelNoche(GameObject panel)
    {
        _panelNoche = panel;
    }
}