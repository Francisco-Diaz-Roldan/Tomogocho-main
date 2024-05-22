using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TresEnRayaController : MonoBehaviour
{
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private GameObject _panelHome;
    [SerializeField] private GameObject _panelResetearPuntuacion;
    private bool _isHomeMenuActive = false;

    public void Restart()
    {
        SceneManager.LoadScene("TresEnRayaMinigameScene");
    }

    public void Pause()
    {
        _panelHome.SetActive(true);
        _isHomeMenuActive = true;
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

    public void GoBack()
    {
        _panelHome.SetActive(false);
            Time.timeScale = 1f;
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
