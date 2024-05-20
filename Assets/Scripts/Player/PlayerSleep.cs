using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleep : MonoBehaviour
{
    
    private Animator animator;
    private bool _isSleeping = false;
    private PlayerDead _playerDead;
    [SerializeField] GameObject carita_happy;
    [SerializeField] GameObject carita_comida;
    private Egg _egg;

    //Hago un Get de la propiedad privada y no puede editarse porque voy a poder acceder desde cualquier script
    //y que por seguridad solo se pueda activar desde aquí
    public bool IsSleeping => _isSleeping;

    // Start is called before the first frame update
    void Start()
    {
        _playerDead = GetComponent<PlayerDead>();
        animator = GetComponent<Animator>();
        _egg = GetComponent<Egg>();
    }

    public void ChangeSleepState(bool isSleeping)
    {
        if (_playerDead != null && !_playerDead.IsDead)
        {
            if (_egg == null || !_egg.IsEgg())
            {
                _isSleeping = isSleeping;
            }
        }
        else
        {
            isSleeping = false; // Fuerzo el estado de sueño a falso si el jugador está muerto
        }
        animator.SetBool("SleepTime", isSleeping);
        if (_isSleeping)
        {
            carita_happy.SetActive(false);
            carita_comida.SetActive(false);
        }
    }
}