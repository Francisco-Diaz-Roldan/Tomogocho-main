using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class Sleep : MonoBehaviour
{
    [SerializeField] private Image _sleepBar;
    [SerializeField] private GameObject _panelNoche;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerHappy _playerHappy;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private Hapiness _hapiness;
    [SerializeField] private GameObject _panelHome; // Referencia al panel de Home
    [SerializeField] private GameObject _panelMinijuegos; // Referencia al panel de Minijuegos
    [SerializeField] private Button _hungerButton; // Referencia al bot�n de Hunger
    [SerializeField] private Button _sleepButton; // Referencia al bot�n de Sleep
    [SerializeField] private Button _minigameButton; // Referencia al bot�n de Minigame
    [SerializeField] private TMP_Text _timeRemainingText;

    public PlayerData playerData;
    private bool _eggIsOpened;
    private bool isResting = false;
    private float restInterval = 1f;
    private bool isNightTime = false;
    private bool _waitForWakeUp = false;
    private float _secondsUntilAwake = 15f;
    private float _timeRemaining;

    // Cambiar el enum SleepReason a public
    public enum SleepReason
    {
        LowSleep,
        NightTime
    }

    private void Start()
    {
        _sleepBar.fillAmount = playerData.SleepPercent;
        if (_playerDead == null) { return; }
        if (_sleepBar.fillAmount < 0.5f) { _hapiness.NotifyLowSleep(); }
        if (!_playerDead.IsDead) InvokeRepeating(nameof(DecreaseSleep), 1f, 1f); // Llama repetidamente al m�todo que decrementa la barra de sue�o con un intervalo de tiempo de 1 segundo
    }

    private void Update()
    {
        if (_sleepBar.fillAmount < 0.5f && _sleepBar.fillAmount > 0) { _hapiness.NotifyLowSleep(); }
        if (_sleepBar.fillAmount > 0f) { _hapiness.NotifyNoSleep(); }
    }

    private void FixedUpdate()
    {
        ForceSleepAtZero();
    }

    private void ForceSleepAtZero()
    {
        if (_sleepBar.fillAmount <= 0f && !isNightTime && !_waitForWakeUp)
        {
            ForceSleep(SleepReason.LowSleep);
            Invoke(nameof(ForceWakeUpAfterDelay), _secondsUntilAwake); // Espera 15 segundos para que despierte
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

        if (!isResting && !_playerDead.IsDead) // Solo baja si no se est� descansando
        {
            _sleepBar.fillAmount -= 0.01f; // Decrementa el porcentaje de sue�o
            _sleepBar.fillAmount = Mathf.Max(_sleepBar.fillAmount, 0f); // Asegura que el valor no sea menor que 0
            playerData.SleepPercent = _sleepBar.fillAmount;
        }
    }

    // M�todo para iniciar o detener el descanso al pulsar un bot�n
    public void RestCreature()
    {
        if (!isNightTime && (!_panelHome.activeSelf && !_panelMinijuegos.activeSelf))
        {
            isResting = !isResting; // Cambio el estado de descanso al presionar el bot�n

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

    // M�todo para forzar el descanso
    public void ForceSleep(SleepReason reason)
    {
        if (!isResting)
        {
            isResting = true;
            isNightTime = reason == SleepReason.NightTime;
            if (!_panelHome.activeSelf) // Verifica si el panel de Home no est� activo
            {
                StartResting();
                _hungerButton.interactable = false;
                _sleepButton.interactable = false;
                _minigameButton.interactable = true; // Permitir interacci�n con el bot�n de Minijuegos durante la noche
                _timeRemainingText.gameObject.SetActive(true);
                _timeRemaining = _secondsUntilAwake; // Inicializa el tiempo restante
                InvokeRepeating(nameof(UpdateTimer), 0f, 1f); // Actualiza el tiempo restante cada segundo

                if (reason == SleepReason.LowSleep)
                {
                    _timeRemainingText.text = "Tomogocho est� agotado. Espera " + Mathf.RoundToInt(_timeRemaining).ToString() + " segundos para que se despierte";
                }
                else if (reason == SleepReason.NightTime)
                {
                    _timeRemainingText.text = "Penne";
                }
            }
        }
    }

    // M�todo para forzar el despertar
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
            _timeRemainingText.gameObject.SetActive(false);
            CancelInvoke(nameof(UpdateTimer)); // Cancela la actualizaci�n del tiempo restante
        }
    }

    // M�todo para iniciar el descanso
    private void StartResting()
    {
        if (!_panelHome.activeSelf) // Verifica si el panel de Home no est� activo
        {
            _panelNoche.SetActive(true); // Activo el GameObject Panel Noche al comenzar a descansar
            CancelInvoke(nameof(DecreaseSleep)); // Detengo la repetici�n del m�todo que decrementa la barra de sue�o
            InvokeRepeating(nameof(IncreaseSleep), 0f, restInterval); // Comienza a llamar repetidamente al m�todo que incrementa la barra de sue�o con un intervalo
            _playerSleep.ChangeSleepState(true); // Activo la animaci�n "Sleeping" en el Animator
            _playerHappy.ActivateHappyFace(false); // Desactivo la cara feliz del jugador
        }
    }

    // M�todo para detener el descanso
    private void StopResting()
    {
        if (!isNightTime) // Solo desactiva el panel si no es de noche
        {
            if (!_panelHome.activeSelf) // Verifica si el panel de Home no est� activo
            {
                _panelNoche.SetActive(false); // Desactivo el GameObject Panel Noche al dejar de descansar
            }
        }
        CancelInvoke(nameof(IncreaseSleep)); // Detengo la repetici�n del m�todo que incrementa la barra de sue�o
        InvokeRepeating(nameof(DecreaseSleep), 1f, 1f); // Reinicio la repetici�n del m�todo que decrementa la barra de sue�o
        _playerSleep.ChangeSleepState(false); // Desactivo la animaci�n "Sleeping" en el Animator
    }

    // M�todo llamado repetidamente cuando se est� descansando
    public void IncreaseSleep()
    {
        if (!_playerDead.IsDead)
        {
            float incrementSpeed = 0.025f;

            _sleepBar.fillAmount = Mathf.Lerp(_sleepBar.fillAmount, 1f, incrementSpeed); // Calculo el nuevo valor de fillAmount de manera m�s suave usando Lerp
            _sleepBar.fillAmount = Mathf.Min(_sleepBar.fillAmount, 1f); // Asegura que el valor no supere 1
            playerData.SleepPercent = _sleepBar.fillAmount; // Guardo el valor actual de _sleepBar.fillAmount en SleepPercent de PlayerData
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

    private void UpdateTimer()
    {
        _timeRemaining -= 1f;

        if (_timeRemaining <= 0)
        {
            ForceWakeUp(); // Despierta al personaje cuando el tiempo restante llega a cero
            return;
        }

       if (isNightTime)
    {
        int totalSecondsUntilMorning = (7 * 3600) - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
        if (totalSecondsUntilMorning < 0)
        {
            totalSecondsUntilMorning += 24 * 3600; // Si ya es despu�s de las 7 AM, sumamos 24 horas para calcular el tiempo hasta ma�ana.
        }
        int hoursUntilMorning = totalSecondsUntilMorning / 3600;
        int minutesUntilMorning = (totalSecondsUntilMorning % 3600) / 60;
        int secondsUntilMorning = totalSecondsUntilMorning % 60;

        string wakeupTime = string.Format("El Tomogocho se despertar� a las 7:00h. A�n faltan {0} horas, {1} minutos y {2} segundos. para que el Tomogocho se despierte", hoursUntilMorning, minutesUntilMorning, secondsUntilMorning);
        _timeRemainingText.text = wakeupTime;
    }
        else
        {
            _timeRemainingText.text = "El Tomogocho est� agotado. Espera " + Mathf.RoundToInt(_timeRemaining).ToString() + " segundos para que se despierte";
        }
    }
}
