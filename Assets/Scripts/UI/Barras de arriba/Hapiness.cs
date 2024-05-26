using UnityEngine;
using UnityEngine.UI;

public class Hapiness : MonoBehaviour
{
    [SerializeField] private Image _hapinessBar;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerPoop _playerPoop;

    public PlayerData _playerData;

    private bool _eggIsOpened;
    private bool _lowSleepNotified = false;
    private bool _noSleepNotified = false;
    private bool _bitHungry = false;
    private bool _reallytHungry = false;

    public float HapinessBarPercent => _hapinessBar.fillAmount;

    private void Start()
    {
        _hapinessBar.fillAmount = _playerData.HapinessPercent;
        InvokeRepeating(nameof(UpdateBar), 1f, 1f); // Llama repetidamente a UpdateBar con un intervalo de 1 segundo
    }

    public void StartBar()
    {
        _eggIsOpened = true;
    }

    public void MakeFeelHappyCreature()
    {
        _hapinessBar.fillAmount = Mathf.Min(1f, _hapinessBar.fillAmount + .1f);
    }

    private void UpdateBar()
    {
        if (!_eggIsOpened || _playerDead.IsDead)
        {
            return;
        }

        bool isSleeping = _playerSleep.IsSleeping;
        int activePoopCount = _playerPoop.GetActivePoopCount();

        float decreaseAmount = 0f;

        if (!isSleeping && !_playerDead.IsDead) //Aquí es donde baja la barra de vida
        {
            decreaseAmount = 0.005f + (activePoopCount * 0.005f);

            if (_bitHungry)
            {
                decreaseAmount *= 1.002f;
                _bitHungry = false;
            }
            else if (_reallytHungry)
            {
                decreaseAmount *= 1.005f;
                _reallytHungry = false;
            }

            if (_lowSleepNotified)
            {
                decreaseAmount *= 1.002f;
                _lowSleepNotified = false;
            }
            else if (_noSleepNotified)
            {
                decreaseAmount *= 1.005f;
                _noSleepNotified = false;
            }
        }

        _hapinessBar.fillAmount -= decreaseAmount;
        _hapinessBar.fillAmount = Mathf.Max(_hapinessBar.fillAmount, 0f);

        if (!_playerDead.IsDead && !isSleeping)
        {
            _playerData.HapinessPercent = _hapinessBar.fillAmount;
        }
    }

    public void NotifyLowSleep()
    {
        _lowSleepNotified = true;
    }

    public void NotifyNoSleep()
    {
        _noSleepNotified = true;
    }

    public void NotifyBitHungry()
    {
        _bitHungry = true;
    }

    public void NotifyReallyHungry()
    {
        _reallytHungry = true;
    }
}