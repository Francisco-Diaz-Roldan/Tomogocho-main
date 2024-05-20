using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TresEnRayaController : MonoBehaviour
{
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private GameObject _panelHome;
    public void Restart()
    {
        SceneManager.LoadScene("TresEnRayaMinigameScene");
    }

    public void Pause()
    {
        _panelHome.SetActive(true);
    }

    public void GoToMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void GoToGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void GoBack()
    {
        _panelHome.SetActive(false);
    }

    public void OpenPanelResultadoAbdClosePanelHome()
    {
        _panelResultado.SetActive(true);
        _panelHome.SetActive(false);
    }
}
