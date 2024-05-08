using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Velocidad de movimiento")]
    private float _speed;
    private Vector2 _centerPlayerPosition;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private Animator _playerAnimator;
    private bool _inZone = true;

    private bool _isMoving = false;
    private float _timeSinceLastTurn = 1f;
    private float _turnInterval = 1f; // Intervalo de tiempo para cambiar la dirección
    private float _minTurnAngle = 100f; // Ángulo mínimo de giro
    private float _maxTurnAngle = 192f; // Ángulo máximo de giro
    private bool _isHappy = false;

    private PlayerSleep _sleep;

    private void Awake()
    {
        _sleep = GetComponent<PlayerSleep>();
        _centerPlayerPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _direction = Vector2.zero;
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isHappy || _sleep.IsSleeping) { return; }

        if ( !_inZone) { 
            OnReturnPosition();
            _isMoving = true;
            return;
        }
        if (!_isMoving)
        {
            SetRandomDirection();
            _isMoving = true;
        }

        // Contador para decidir cuándo dar una vuelta
        _timeSinceLastTurn += Time.deltaTime;
        if (_timeSinceLastTurn >= _turnInterval)
        {
            RotateDirection();
            _timeSinceLastTurn = 0f; // Reiniciar el contador
        }
    }

    private void FixedUpdate()
    {
        if ( _isHappy || _sleep.IsSleeping) { return; }
        Move();        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerCage"))
        {
            _inZone = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerCage"))
        {
            Invoke(nameof(InZoneTrue), 1f);
        }
    }

    private void OnReturnPosition()
    {
        _direction = _centerPlayerPosition - (Vector2)transform.position;
        _direction = _direction.normalized;
        _playerAnimator.SetFloat("X", _direction.x);
        _playerAnimator.SetFloat("Y", _direction.y);
        _playerAnimator.SetFloat("Speed", _direction.sqrMagnitude);
    }

    private void InZoneTrue() {
        _inZone = true;
    }

    private void SetRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        _direction = new Vector2(x, y).normalized;

        _playerAnimator.SetFloat("X", x);
        _playerAnimator.SetFloat("Y", y);
        _playerAnimator.SetFloat("Speed", _direction.sqrMagnitude);
    }

    private void Move()
    {
        if (_direction.magnitude == 0)
        {
            _isMoving = false;
            return;
        }

        _rb.position = _rb.position + _direction * _speed * Time.deltaTime;
    }

    private void RotateDirection()
    {
        // Rotar la dirección actual
        float angle = Random.Range(_minTurnAngle, _maxTurnAngle) * (Random.value > 0.5f ? 1f : -1f); // Girar en dirección aleatoria
        _direction = Quaternion.Euler(0, 0, angle) * _direction;

        // Normalizar la dirección
        _direction.Normalize();

        // Actualizar la animación
        _playerAnimator.SetFloat("X", _direction.x);
        _playerAnimator.SetFloat("Y", _direction.y);
    }

    public void SetHappyFace(bool isHappy)
    {
        _isHappy = isHappy;
        if (isHappy )
        {
            _playerAnimator.SetFloat("X", 0);
            _playerAnimator.SetFloat("Y", -1);
            _playerAnimator.SetFloat("Speed", 0);
        } else
        {
            _playerAnimator.SetFloat("X", _direction.x);
            _playerAnimator.SetFloat("Y", _direction.y);
            _playerAnimator.SetFloat("Speed", _direction.sqrMagnitude);
        }
    }
}