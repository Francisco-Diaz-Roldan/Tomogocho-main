using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _botonHome;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private GameObject _panelMinijuegos;

    private PlayerDead _playerDead;

    private bool _isHomeMenuActive = false;

    private void Start()
    {
        _playerDead = FindObjectOfType<PlayerDead>();
        if (_playerDead == null) { return; }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        _panelHome.SetActive(true);
        _panelMinijuegos.SetActive(false);
        _isHomeMenuActive = true;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        if (_panelHome != null)
        {
            _panelHome.SetActive(false);
            _isHomeMenuActive = false;
        }
    }

    public void ToggleHomeMenu()
    {
        if (_isHomeMenuActive)
        {
            Reanudar();
        }
        else
        {
            Pausa();
        }
    }

    public void TogglePanelMinijuegos()
    {
        if (_panelMinijuegos.activeSelf)
        {
            ClosePanelMinijuegos();
        }
        else
        {
            OpenPanelMinijuegos();
        }
    }

    public void OpenPanelMinijuegos()
    {
        Time.timeScale = 0f;
        _panelMinijuegos.SetActive(true);
        _panelHome.SetActive(false);
        _isHomeMenuActive = false;
    }

    public void ClosePanelMinijuegos()
    {
        Time.timeScale = 1f;
        _panelMinijuegos.SetActive(false);
    }

    public void GoToMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void GoToChromeMiniGameScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAndPause("ChromeMiniGameScene"));
    }

    public void GoToTresEnRayaMiniGameScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAndPause("TresEnRayaMiniGameScene"));
    }

    public void GoToPiedraPapelTijerasMiniGameScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAndPause("PiedraPapelTijerasMiniGameScene"));
    }

    private System.Collections.IEnumerator LoadSceneAndPause(string sceneName) // Método para cargar la escena y pausarla
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName); // Cargo la nueva escena de forma asíncrona

        while (!asyncLoad.isDone) { yield return null; } // Espero hasta que la escena se haya cargado completamente
    }
}