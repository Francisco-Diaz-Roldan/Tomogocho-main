using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    [SerializeField] private Image _hungryBar;
    [SerializeField] private PlayerDead _playerDead;
    [SerializeField] private PlayerSleep _playerSleep;
    [SerializeField] private PlayerEating _playerEating;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Hapiness _hapiness;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private GameObject _panelMinijuegos;

    public PlayerData _playerData;

    private bool _eggIsOpened;

    private void Start()
    {
        _hungryBar.fillAmount = _playerData.HungerPercent;
        if (_playerDead == null || _playerSleep == null) { return; }
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping) InvokeRepeating(nameof(UpdateBar), 1f, 1f); // Llama repetidamente a UpdateBar con un intervalo de 1 segundo si el jugador no está muerto
    }

    public void StartBar()
    {
        _eggIsOpened = true;
    }

    private void Update()
    {
        if (_hungryBar.fillAmount < 0.5f && _hungryBar.fillAmount > 0) { _hapiness.NotifyBitHungry(); }
        if (_hungryBar.fillAmount > 0f) { _hapiness.NotifyReallyHungry(); }
    }

    public void FeedCreature()
    {
        if (!_playerDead.IsDead && !_playerSleep.IsSleeping && !_panelHome.activeSelf && !_panelMinijuegos.activeSelf)
        {
            _playerEating.ActivateEatingFace(true);
            _hungryBar.fillAmount = Mathf.Min(1f, _hungryBar.fillAmount + .1f);
            StartCoroutine(DisactivateEatingCoroutine());
        }
    }

    private IEnumerator DisactivateEatingCoroutine() // Corrutina para desactivar la cara del Tomogocho cuando come después de un tiempo
    {
        yield return new WaitForSeconds(1.5f);
        _playerEating.ActivateEatingFace(false);
    }

    private void UpdateBar() // Actualiza la barra de hambre
    {
        if (!_eggIsOpened) { return; }

        if (!_playerDead.IsDead && !_playerSleep.IsSleeping && !_panelHome.activeSelf && !_panelMinijuegos.activeSelf)
        {
            _hungryBar.fillAmount -= .015f;
            _hungryBar.fillAmount = Mathf.Max(_hungryBar.fillAmount, 0f);

            if (_playerData != null)
            {
                _playerData.HungerPercent = _hungryBar.fillAmount;
            }

            _playerMovement.SetHungry(_hungryBar.fillAmount == 0f);
        }
    }
}