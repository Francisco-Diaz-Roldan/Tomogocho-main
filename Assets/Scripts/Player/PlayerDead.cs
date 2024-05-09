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
    }

    void Update()
    {
        if (_hapiness.HapinessBarPercent <= 0f)
        {
            _animator.SetBool("IsAlive", false);
        } else
        {
            _animator.SetBool("IsAlive", true);
        }
    }
    public void ChangeDeadState(bool isDead)
    {
        _isDead = isDead;
        _animator.SetBool("IsAlive", !isDead);
    }


}
