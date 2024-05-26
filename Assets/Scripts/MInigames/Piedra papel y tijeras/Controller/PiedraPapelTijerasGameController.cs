using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiedraPapelTijerasGameController : MonoBehaviour
{
    [SerializeField] private GameObject _panelResetearPuntuacion;
    [SerializeField] private GameObject _panelMiniGame;
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private GameObject _panelHome;

    private bool _isHomeMenuActive = false;
    private bool _isMiniGameActive = false;

    public void Restart()
    {
        if (GameObject.FindObjectOfType<PiedraPapelTijeras>().partidaTerminada) // Comprueba si la partida ha terminado antes de reiniciar 

        {
            SceneManager.LoadScene("PiedraPapelTijerasMinigameScene");
        }
        else
        {
            _panelResultado.SetActive(false);
        }
    }

    public void Pause()
    {
        _panelHome.SetActive(true);
        _panelMiniGame.SetActive(false);
        _isHomeMenuActive = true;
        _isMiniGameActive = false;
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void OpenPanelResultados()
    {
        _panelResultado.SetActive(true);
    }

    public void ToggleMiniGame()
    {
        if (_isMiniGameActive)
        {
            ClosePanelMiniGame();
        }
        else
        {
            OpenPanelMiniGame();
        }
    }

    public void OpenPanelMiniGame()
    {
        _panelMiniGame.SetActive(true);
        _panelHome.SetActive(false); // Desactiva el panel del Menú
        _isMiniGameActive = true;
        _isHomeMenuActive = false; // Actualiza el estado del Menú
    }

    public void ClosePanelMiniGame()
    {
        _panelMiniGame.SetActive(false);
        _isMiniGameActive = false;
    }

    public void GoBack()
    {
        _panelHome.SetActive(false);
        _panelMiniGame.SetActive(false); // Desactiva el panel de MiniGame
        _isHomeMenuActive = false;
        _isMiniGameActive = false; // Actualiza el estado del MiniGame
    }

    public void ToggleHomeMenu()
    {
        if (_isHomeMenuActive)
        {
            GoBack();
        }
        else
        {
            Pause();
        }
    }

    public void OpenPanelResultado()
    {
        _panelResultado.SetActive(true);
    }

    public void OpenPanelResultadoAndClosePanelHome()
    {
        _panelResultado.SetActive(true);
        _panelResetearPuntuacion.SetActive(false);
        _isHomeMenuActive = false;
    }

    public void OpenPanelMenuResetearPuntuacion()
    {
        _panelResetearPuntuacion.SetActive(true);
    }

    public void ClosePanelMenuResetearPuntuacion()
    {
        _panelResetearPuntuacion.SetActive(false);
    }
}