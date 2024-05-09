using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleep : MonoBehaviour
{
    // Referencia al Animator del personaje para controlar la animación
    private Animator animator;
    private bool _isSleeping = false;

    //Hago un Get de la propiedad privada y no puede editarse porque voy a poder acceder desde cualquier script
    //y que por seguridad solo se pueda activar desde aquí
    public bool IsSleeping => _isSleeping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void ChangeSleepState(bool isSleeping)
    {
        _isSleeping = isSleeping;
        animator.SetBool("SleepTime", isSleeping);
    }
}