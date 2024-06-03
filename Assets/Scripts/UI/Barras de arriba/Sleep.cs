using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private GameObject _panelMinijuegos; // Referencia al panel de Minijuegos
    [SerializeField] private Button _hungerButton; // Referencia al botón de Hunger
    [SerializeField] private Button _sleepButton; // Referencia al botón de Sleep
    [SerializeField] private Button _minigameButton; // Referencia al botón de Minigame
    [SerializeField] private TMP_Text _timeRemainingText;

    public PlayerData playerData;

    private bool _eggIsOpened;
    private bool isResting = false;
    private float restInterval = 1f;
    private bool isNightTime = false;
    private bool _waitForWakeUp = false;
    private float _secondsUntilAwake = 15f;
    private float _timeRemaining;

    // Cambiar el enumerado SleepReason a public
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
        if (!_playerDead.IsDead) InvokeRepeating(nameof(DecreaseSleep), 1f, 1f); // Llama repetidamente al método que decrementa la barra de sueño con un intervalo de tiempo de 1 segundo
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
        // Verifica si la barra de sueño está vacía y la criatura no está descansando
        if (_sleepBar.fillAmount <= 0f && !isResting)
        {
            ForceSleep(SleepReason.LowSleep);
            Invoke(nameof(ForceWakeUpAfterDelay), _secondsUntilAwake);
        }
    }

    private void ForceWakeUpAfterDelay()
    {
        ForceWakeUp();
        _waitForWakeUp = false;
    }

    public void StartBar()
    {
        _eggIsOpened = true;
    }

    private void DecreaseSleep()
    {
        if (!_eggIsOpened) { return; }

        if (!isResting && !_playerDead.IsDead)
        {
            _sleepBar.fillAmount -= 0.01f;
            _sleepBar.fillAmount = Mathf.Max(_sleepBar.fillAmount, 0f);
            playerData.SleepPercent = _sleepBar.fillAmount;
        }
    }

    // Método para iniciar o detener el descanso al pulsar un botón
    public void RestCreature()
    {
        if (!isNightTime && (!_panelHome.activeSelf && !_panelMinijuegos.activeSelf))
        {
            isResting = !isResting;

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

    public void ForceSleep(SleepReason reason)
    {
        if (!isResting)
        {
            isResting = true;
            isNightTime = reason == SleepReason.NightTime;
            if (!_panelHome.activeSelf)
            {
                StartResting();
                _hungerButton.interactable = false;
                _sleepButton.interactable = false;
                _minigameButton.interactable = true;
                _timeRemainingText.gameObject.SetActive(true);
                _timeRemaining = _secondsUntilAwake;
                InvokeRepeating(nameof(UpdateTimer), 0f, 1f);

                if (reason == SleepReason.LowSleep)
                {
                    _timeRemainingText.text = "Tomogocho está agotado. Espera " + Mathf.RoundToInt(_timeRemaining).ToString() + " segundos para que se despierte";
                }
                else if (reason == SleepReason.NightTime)
                {
                    _timeRemainingText.text = "Penne";
                }
            }
        }
    }

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
            CancelInvoke(nameof(UpdateTimer));
        }
    }

    private void StartResting()
    {
        if (!_panelHome.activeSelf)
        {
            _panelNoche.SetActive(true);
            CancelInvoke(nameof(DecreaseSleep));
            InvokeRepeating(nameof(IncreaseSleep), 0f, restInterval);
            _playerSleep.ChangeSleepState(true);
            _playerHappy.ActivateHappyFace(false);
        }
    }

    private void StopResting()
    {
        if (!isNightTime)
        {
            if (!_panelHome.activeSelf)
            {
                _panelNoche.SetActive(false);
            }
        }
        CancelInvoke(nameof(IncreaseSleep));
        InvokeRepeating(nameof(DecreaseSleep), 1f, 1f);
        _playerSleep.ChangeSleepState(false);
    }

    public void IncreaseSleep()
    {
        if (!_playerDead.IsDead)
        {
            float incrementSpeed = 0.025f;

            _sleepBar.fillAmount = Mathf.Lerp(_sleepBar.fillAmount, 1f, incrementSpeed);
            _sleepBar.fillAmount = Mathf.Min(_sleepBar.fillAmount, 1f);
            playerData.SleepPercent = _sleepBar.fillAmount;
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
            ForceWakeUp();
            return;
        }

        if (isNightTime)
        {
            int totalSecondsUntilMorning = (7 * 3600) - (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second);
            if (totalSecondsUntilMorning < 0)
            {
                totalSecondsUntilMorning += 24 * 3600; // Si ya es después de las 7 AM, sumamos 24 horas para calcular el tiempo hasta mañana.
            }
            int hoursUntilMorning = totalSecondsUntilMorning / 3600;
            int minutesUntilMorning = (totalSecondsUntilMorning % 3600) / 60;
            int secondsUntilMorning = totalSecondsUntilMorning % 60;

            string wakeupTime = string.Format("El Tomogocho se despertará a las 7:00h. Aún faltan {0} horas, {1} minutos y {2} segundos. para que el Tomogocho se despierte", hoursUntilMorning, minutesUntilMorning, secondsUntilMorning);
            _timeRemainingText.text = wakeupTime;
        }
        else
        {
            _timeRemainingText.text = "El Tomogocho está agotado. Espera " + Mathf.RoundToInt(_timeRemaining).ToString() + " segundos para que se despierte";
        }
    }
}
