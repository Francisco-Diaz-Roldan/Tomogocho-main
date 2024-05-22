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
        SceneManager.LoadScene("PiedraPapelTijerasMinigameScene");
    }
    public void Pause()
    {
        _panelHome.SetActive(true);
        _isHomeMenuActive = true;
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
        _isMiniGameActive = !_isMiniGameActive;
        _panelMiniGame.SetActive(_isMiniGameActive);
    }

    public void OpenPanelMiniGame()
    {
        _panelMiniGame.SetActive(true);
        _isMiniGameActive = true;
    }

    public void ClosePanelMiniGame()
    {
        _panelMiniGame.SetActive(false);
        _isMiniGameActive = false;
    }
    public void GoBack()
    {
        _panelHome.SetActive(false);
        _isHomeMenuActive = false;
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
