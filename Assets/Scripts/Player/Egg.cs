using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Egg : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private float _rotationDuration = 2f;
    [SerializeField] private GameObject _player;
    [SerializeField] private Sleep _sleepBar;
    [SerializeField] private Hunger _hungerBar;
    [SerializeField] private Hapiness _hapinessBar;
    [SerializeField] private Button _sleepButton;
    [SerializeField] private Button _hungerButton;
    [SerializeField] private Button _minigamesButton;
    [SerializeField] private TMP_Text _timeRemainingText;
    [SerializeField] private AudioClip _touchSound;

    private AudioSource _audioSource;

    private bool isEgg = true;

    public bool IsEgg()
    {
        return isEgg;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_playerData.EggHasBeenOpened())
        {
            OpenEgg();
        }
        EggMovement();
    }

    void Update()
    {
        if (_playerData != null)
        {
            if (_playerData.EggHasBeenOpened())
            {
                OpenEgg();
            }
            _playerData.LifeTimeInSeconds += Time.deltaTime;
            UpdateTimeRemainingText();
        }
    }

    private void EggMovement()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(0, 0, -25), _rotationDuration));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 25), _rotationDuration));
        sequence.SetLoops(-1, LoopType.Yoyo);
        sequence.Play();
    }

    private void OpenEgg()
    {
        ActivateBars();
        ActivateButtons();
        _player.SetActive(true);
        DOTween.KillAll();
        gameObject.SetActive(false);
        isEgg = false;
    }

    private void ActivateBars()
    {
        _sleepBar.StartBar();
        _hungerBar.StartBar();
        _hapinessBar.StartBar();
    }

    private void ActivateButtons()
    {
        _sleepButton.interactable = true;
        _hungerButton.interactable = true;
        _minigamesButton.interactable = true;
    }

    private void UpdateTimeRemainingText()
    {
        float timeRemaining = _playerData.TimeToOpenEgg - _playerData.LifeTimeInSeconds;
        timeRemaining = Mathf.Max(timeRemaining, 0);  // Me aseguro de que no sea negativo

        if (isEgg)
        {
            if (timeRemaining <= 0)
            {
                _timeRemainingText.gameObject.SetActive(false);
            }
            else
            {
                // Convierto el tiempo restante a segundos enteros
                _timeRemainingText.gameObject.SetActive(true);
                _timeRemainingText.text = $"Incubando huevo\r\nFaltan: {timeRemaining:N0} segundos para abrirse";
            }
        }
        else
        {
            _timeRemainingText.gameObject.SetActive(false); 
        }
    }

    public void TouchEgg()
    {
        _playerData.TimeToOpenEgg -= 1f;
        _audioSource.PlayOneShot(_touchSound);
    }
}