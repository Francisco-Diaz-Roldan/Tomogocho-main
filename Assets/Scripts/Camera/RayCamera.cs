using UnityEngine;

public class RayCamera : MonoBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private AudioClip _cleanedPooSound;
    [SerializeField] private AudioClip _touchedPlayerSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPosition = Input.mousePosition;

            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
            }

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.nearClipPlane));

            // Ajustar la posición z para que coincida con el plano de juego 2D
            worldPosition.z = 0f;

            // Lanza un rayo desde la posición del input para detectar colisiones
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            // Comprueba las colisiones con los objetos con tags específicas
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                HandlePlayerHit(hit.collider.gameObject);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Poo"))
            {
                HandlePooHit(hit.collider.gameObject);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Egg"))
            {
                Egg egg = hit.collider.gameObject.GetComponent<Egg>();
                egg.TouchEgg();
            }
        }
    }

    private void HandlePooHit(GameObject poo)
    {
        // Desactiva el objeto primero
        poo.SetActive(false);

        // Reproduce el sonido al tocar la caquita
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(_cleanedPooSound);
    }

    private void HandlePlayerHit(GameObject player)
    {
        PlayerSleep playerSleep = player.GetComponent<PlayerSleep>();
        PlayerHappy playerHappy = player.GetComponent<PlayerHappy>();
        PlayerDead playerDead = player.GetComponent<PlayerDead>();

        if (playerSleep.IsSleeping) { return; }

        if (playerDead.IsDead)
        {
            _panelGameOver.SetActive(true);
            if (_playerData.MostOldTomogochoTime < _playerData.LifeTimeInSeconds)
            {
                _playerData.MostOldTomogochoTime = _playerData.LifeTimeInSeconds;
            }
            return;
        }

        if (!_panelHome.activeSelf)
        {
            playerHappy.ActivateHappyFace(true);
        }

        Hapiness hapiness = FindObjectOfType<Hapiness>();
        if (hapiness != null && !_panelHome.activeSelf)
        {
            hapiness.MakeFeelHappyCreature();
        }

        _audioSource.PlayOneShot(_touchedPlayerSound);
    }
}