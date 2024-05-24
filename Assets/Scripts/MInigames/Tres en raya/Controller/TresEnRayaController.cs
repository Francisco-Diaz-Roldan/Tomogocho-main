using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TresEnRayaController : MonoBehaviour
{
    [SerializeField] private GameObject _panelResetearPuntuacion;
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private GameObject _panelMiniGame;
    [SerializeField] private GameObject _panelHome;
    private bool _isHomeMenuActive = false;
    private bool _isMiniGameActive = false;

    public void Restart()
    {
        SceneManager.LoadScene("TresEnRayaMinigameScene");
    }

    public void Pause()
    {
        _panelHome.SetActive(true);
        _panelMiniGame.SetActive(false); // Desactiva el panel de MiniGame
        _isHomeMenuActive = true;
        _isMiniGameActive = false; // Actualiza el estado del MiniGame
    }

    public void GoToMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void GoToGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void OpenPanelMiniGame()
    {
        _panelMiniGame.SetActive(true);
        _panelHome.SetActive(false); // Desactiva el panel del Men�
        _isMiniGameActive = true;
        _isHomeMenuActive = false; // Actualiza el estado del Men�
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
        Time.timeScale = 1f;
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

    public void ToggleMiniGameButton()
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
