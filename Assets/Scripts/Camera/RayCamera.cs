using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCamera : MonoBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private PlayerData _playerData;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPosition = Input.mousePosition;

            // Si la entrada es un toque en la pantalla, obtener la posición del toque
            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
            }

            // Convertir la posición del input a coordenadas del mundo
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.nearClipPlane));

            // Agregar un Debug.Log para verificar la posición de entrada
            Debug.Log("Input position: " + inputPosition + ", World position: " + worldPosition);

            // Ajustar la posición z para que coincida con el plano de juego 2D
            worldPosition.z = 0f;

            // Lanza un rayo desde la posición del input para detectar colisiones
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }

            // Verificar colisión con objetos específicos
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                HandlePlayerHit(hit.collider.gameObject);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Poo"))
            {
                hit.collider.gameObject.SetActive(false);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Egg"))
            {
                Debug.Log("Egg hit detected");
                _playerData.TimeToOpenEgg -= 1f;
            }
        }
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
    }
}
/*public class RayCamera : MonoBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private PlayerData _playerData;

    void Update()
    {
        // Verificar si hay entrada de ratón o toque en la pantalla
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 inputPosition = Input.mousePosition;

            // Si la entrada es un toque en la pantalla, obtener la posición del toque
            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
            }

            // Convertir la posición del input a coordenadas del mundo
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, Camera.main.nearClipPlane));

            // Agregar un Debug.Log para verificar la posición de entrada
            Debug.Log("Input position: " + worldPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                GameObject player = hit.collider.gameObject;
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

                Hapiness hapiness = FindObjectOfType<Hapiness>(); // Obtengo el componente Hapiness del objeto que tiene el script Hapiness ya que busca el primer objeto con el script Hapiness en la escena

                if (hapiness != null && !_panelHome.activeSelf)
                {
                    hapiness.MakeFeelHappyCreature();  // Aumenta la barra de felicidad
                }
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Poo"))
            {
                hit.collider.gameObject.SetActive(false);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Egg"))
            {
                _playerData.TimeToOpenEgg -= 1f;
            }
        }
    }
}*/

/*public class RayCamera : MonoBehaviour
{
    [SerializeField] private GameObject _panelGameOver;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private PlayerData _playerData;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si se hace clic izquierdo
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);  // Se lanza un rayo desde la posición del ratón en la pantalla

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                GameObject player = hit.collider.gameObject;
                PlayerSleep playerSleep = player.GetComponent<PlayerSleep>();
                PlayerHappy playerHappy = player.GetComponent<PlayerHappy>();
                PlayerDead playerDead = player.GetComponent<PlayerDead>();
                if (playerSleep.IsSleeping) { return; }
                if (playerDead.IsDead) {
                    _panelGameOver.SetActive(true);
                    if(_playerData.MostOldTomogochoTime < _playerData.LifeTimeInSeconds)
                    {
                        _playerData.MostOldTomogochoTime = _playerData.LifeTimeInSeconds;
                    }
                    return;
                }

                if (!_panelHome.activeSelf)
                {
                    playerHappy.ActivateHappyFace(true);
                }

                Hapiness hapiness = FindObjectOfType<Hapiness>(); // Obtengo el componente Hapiness del objeto que tiene el script Hapiness ya que busca el primer objeto con el script Hapiness en la escena

                if (hapiness != null && !_panelHome.activeSelf)
                {
                    hapiness.MakeFeelHappyCreature();  // Aumenta la barra de felicidad
                }
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Poo"))
            {
                hit.collider.gameObject.SetActive(false);
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Egg"))
            {
                _playerData.TimeToOpenEgg -= 1f;
            }
        }
    }
}*/
