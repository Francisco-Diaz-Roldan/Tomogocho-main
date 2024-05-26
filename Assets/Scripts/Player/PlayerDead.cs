using UnityEngine;
using UnityEngine.UI;

public class PlayerDead : MonoBehaviour
{
    //Referencia al Script de Hapiness
    [SerializeField]private Hapiness _hapiness;
    [SerializeField] private Button _botonComida;
    [SerializeField] private Button _botonSueno;
    [SerializeField] private Button _botonMinijuegos;
    [SerializeField] private AudioClip _deathSound;

    private AudioSource _audioSource;
    private Animator _animator;

    private bool _isDead = false;
    private bool _deathSoundPlayed = false;

    public bool IsDead => _isDead;

    void Start()
    {
       _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckHappiness(); // Comprueba el estado de la felicidad/vida para determinar si el jugador está muerto
    }

    private void CheckHappiness()
    {
        if (_hapiness != null && _hapiness.HapinessBarPercent <= 0f)
        {
            SetDead(true);
            _botonMinijuegos.interactable = false;
            _botonSueno.interactable = false;
            _botonComida.interactable = false;
        }
    }

    public void SetDead(bool isDead)
    {
        _isDead = isDead;
        _animator.SetBool("IsAlive", !isDead);
        if (!_deathSoundPlayed && isDead) 
        {
            _audioSource.PlayOneShot(_deathSound);
            _deathSoundPlayed = true;
        }
    }
}