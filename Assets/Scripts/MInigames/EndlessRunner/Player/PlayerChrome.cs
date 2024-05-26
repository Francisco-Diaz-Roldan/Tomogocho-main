using UnityEngine;
using UnityEngine.UI;

public class PlayerChrome : MonoBehaviour
{
    [SerializeField] private float _upForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private float _fallMultiplier = 2.5f; // Multiplicador de la gravedad al caer
    [SerializeField] private float _lowJumpMultiplier = 2f; // Multiplicador de la gravedad al hacer un salto corto
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _deadSound;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AudioSource _audioSource;

    private bool _isPlaying;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _isPlaying = false;
        _jumpButton.onClick.AddListener(Jump);
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _ground);

        if (isGrounded) _animator.SetFloat("X", 1f);

        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigidbody2D.velocity.y > 0 && !Input.GetMouseButton(0))
        {
            _rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void StartGame()
    {
        _isPlaying = true;
    }

    public void StopGame()
    {
        _isPlaying = false;
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
            StopGame();
            _audioSource.PlayOneShot(_deadSound);
        }
    }

     public void Jump()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _ground);
        if (_isPlaying && isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _upForce);
            _audioSource.PlayOneShot(_jumpSound);
        }
    }
}