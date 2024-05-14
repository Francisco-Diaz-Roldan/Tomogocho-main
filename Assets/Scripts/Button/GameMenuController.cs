using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private PlayerData _playerData;


    /*public void QuitGame()
    {
        // Para salir de la app
        Application.Quit();
    }*/

    public void GoToMenu()
    {
        _panelMenu.SetActive(true);
    }

    public void GoBack()
    {
        // Para volver atrás a la escena principal
        _panelMenu.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void StartNewGame()
    {
        ControlGame();
        // Para ir a la escena de juego
        //TODO hacer que se carguen los datos (controlar también que estén vacíos)
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    private void ControlGame()
    {
        if (_playerData.HasDied())
        {
            _playerData.ResetValues();
        }
    }
}
