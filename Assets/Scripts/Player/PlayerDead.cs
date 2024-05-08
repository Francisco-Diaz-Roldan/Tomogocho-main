using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    //Referencia al Script de Hapiness
    [SerializeField]private Hapiness _hapiness;
    private Animator _animator;

    void Start()
    {
       _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hapiness.HapinessBarPercent <= 0f)
        {
            _animator.SetBool("IsAlive", false);
        }
    }
}
