using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChrome : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _gameOverPanel;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _ground);
        if (isGrounded) _animator.SetFloat("X", 1f);

        if (Input.GetMouseButtonDown(0))
        {
            if (isGrounded)
            {
                _rigidbody2D.AddForce(Vector2.up * _upForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Poo") || other.CompareTag("Lapida"))
        {
            Time.timeScale = 0f;
            _gameOverPanel.SetActive(true);
        }
    }
}