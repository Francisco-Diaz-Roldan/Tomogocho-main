using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    //Referencia al Script de Hapiness
    [SerializeField]private Hapiness _hapiness;
    private Animator _animator;
    private bool _isDead = false;

    public bool IsDead => _isDead;

    void Start()
    {
       _animator = GetComponent<Animator>();
        //coger tambi�n _hapiness
    }

    private void Update()
    {
        CheckHappiness(); // Verifica el estado de la felicidad para determinar si el jugador est� muerto
    }

    private void CheckHappiness()
    {
        if (_hapiness != null && _hapiness.HapinessBarPercent <= 0f)
        {
            SetDead(true); // Marca al jugador como muerto si la felicidad llega a cero
        }/*
        else
        {
            SetDead(false); // Marca al jugador como vivo si la felicidad es mayor que cero
        }*/
    }

    public void SetDead(bool isDead)
    {
        _isDead = isDead;
        _animator.SetBool("IsAlive", !isDead); // Cambia el estado de la animaci�n en base al estado de vida
    }
}
