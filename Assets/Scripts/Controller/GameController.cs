using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _botonHome;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private GameObject _panelMinijuegos;
    private PlayerDead _playerDead;

    private void Start()
    {
        _playerDead = FindObjectOfType<PlayerDead>();
        if (_playerDead == null) { return; }
    }
    public void Pausa()
    {
       // if (_playerDead != null && !_playerDead.IsDead && _panelHome != null)
        //{
            Time.timeScale = 0f;
            _panelHome.SetActive(true);
        //}
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
    }

    public void ClosePanelMinijuegos()
    {
        Time.timeScale = 1f;
        _panelMinijuegos.SetActive(false);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        if (_panelHome != null)
        {
            _panelHome.SetActive(false);
        }
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

    // Método para cargar la escena y pausarla
    private System.Collections.IEnumerator LoadSceneAndPause(string sceneName)
    {
        // Pauso la escena actual
       // Time.timeScale = 0f;

        // Cargo la nueva escena de forma asíncrona
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Espero hasta que la escena se haya cargado completamente
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Pauso la nueva escena una vez que se haya cargado
        //Time.timeScale = 0f;
    }
}
