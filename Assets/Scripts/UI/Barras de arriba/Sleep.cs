using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    [SerializeField] private Image _sleepBar;
    [SerializeField] private GameObject _panelNoche;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerHappy _playerHappy;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private Hapiness _hapiness;

    [SerializeField] private Button _hungerButton; // Referencia al botón de Hunger
    [SerializeField] private Button _sleepButton; // Referencia al botón de Sleep
    [SerializeField] private Button _minigameButton; // Referencia al botón de Minigame


    public PlayerData playerData;
    private bool _eggIsOpened;
    private bool isResting = false;
    private float restInterval = 1f;
    private bool isNightTime = false;
    private bool _waitForWakeUp = false;


    private void Start()
    {
        _sleepBar.fillAmount = playerData.SleepPercent;
        if (_playerDead == null) { return; }
        if (_sleepBar.fillAmount < 0.5f) {_hapiness.NotifyLowSleep(); }
        if (!_playerDead.IsDead) InvokeRepeating(nameof(DecreaseSleep), 1f, 1f);  // Llama repetidamente al método que decrementa la barra de sueño con un intervalo de tiempo de 1 segundo
    }

    private void Update()
    {
        if (_sleepBar.fillAmount < 0.5f && _sleepBar.fillAmount > 0) { _hapiness.NotifyLowSleep(); }
        if (_sleepBar.fillAmount > 0f) { _hapiness.NotifyNoSleep(); }
        if (_sleepBar.fillAmount <= 0f && !isNightTime)
        {
            ForceSleep();
            Invoke(nameof(ForceWakeUpAfterDelay), 15f); // Hacemos que el personaje espere de 15 segundos para que despierte
        }
    }

    private void ForceWakeUpAfterDelay()
     {
         _waitForWakeUp = true; // Establecer que estamos esperando
         ForceWakeUp(); // Forzar al personaje a despertar
     }

    public void StartBar()
    {
        _eggIsOpened = true;
    }

    private void DecreaseSleep()
    {
        if (!_eggIsOpened) { return; }

        if (!isResting && !_playerDead.IsDead) // Solo baja si no se está descansando
        {
            _sleepBar.fillAmount -= 0.01f; // Decrementa el porcentaje de sueño
            _sleepBar.fillAmount = Mathf.Max(_sleepBar.fillAmount, 0f); // Asegura que el valor no sea menor que 0
            playerData.SleepPercent = _sleepBar.fillAmount;
        }
    }

    // Método para iniciar o detener el descanso al pulsar un botón
    public void RestCreature()
    {
        if (!isNightTime) // Solo permite descanso manual si no es de noche
        {
            isResting = !isResting; // Cambio el estado de descanso al presionar el botón

            if (isResting)
            {
                StartResting();
            }
            else
            {
                StopResting();
            }
        }
    }

    // Método para forzar el descanso
    public void ForceSleep()
    {
        if (!isResting)
        {
            isResting = true;
            isNightTime = true;
            StartResting();
            _hungerButton.interactable = false;
            _sleepButton.interactable = false;
            _minigameButton.interactable = false;
        }
    }

    // Método para forzar el despertar
    public void ForceWakeUp()
    {
        if (isResting)
        {
            isResting = false;
            isNightTime = false;
            StopResting();
            _hungerButton.interactable = true;
            _sleepButton.interactable = true;
            _minigameButton.interactable = true;
        }
    }

    // Método para iniciar el descanso
    private void StartResting()
    {
        _panelNoche.SetActive(true); // Activo el GameObject Panel Noche al comenzar a descansar
        CancelInvoke(nameof(DecreaseSleep)); // Detengo la repetición del método que decrementa la barra de sueño
        InvokeRepeating(nameof(IncreaseSleep), 0f, restInterval); // Comienza a llamar repetidamente al método que incrementa la barra de sueño con un intervalo
        _playerSleep.ChangeSleepState(true); // Desactivo la animación "Sleeping" en el Animator
        _playerHappy.ActivateHappyFace(false); // Desactivo la cara feliz del jugador
    }

    // Método para detener el descanso
    private void StopResting()
    {
        if (!isNightTime) // Solo desactiva el panel si no es de noche
        {
            _panelNoche.SetActive(false); // Desactivo el GameObject Panel Noche al dejar de descansar
        }
        CancelInvoke(nameof(IncreaseSleep)); // Detengo la repetición del método que incrementa la barra de sueño
        InvokeRepeating(nameof(DecreaseSleep), 1f, 1f); // Reinicio la repetición del método que decrementa la barra de sueño
        _playerSleep.ChangeSleepState(false); // Desactivo la animación "Sleeping" en el Animator
    }

    // Método llamado repetidamente cuando se está descansando
    public void IncreaseSleep()
    {
        if (!_playerDead.IsDead)
        {
            float incrementSpeed = 0.025f; 

            _sleepBar.fillAmount = Mathf.Lerp(_sleepBar.fillAmount, 1f, incrementSpeed); // Calculo el nuevo valor de fillAmount de manera más suave usando Lerp
            _sleepBar.fillAmount = Mathf.Min(_sleepBar.fillAmount, 1f); // Asegura que el valor no supere 1
            playerData.SleepPercent = _sleepBar.fillAmount;// Guardo el valor actual de _sleepBar.fillAmount en SleepPercent de PlayerData
        }
    }

    public void SetPanelNoche(GameObject panel)
    {
        _panelNoche = panel;
    }
    public bool IsResting()
    {
        return isResting;
    }
}