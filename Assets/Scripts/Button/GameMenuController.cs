using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;

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
        // Para ir a la escena de juego
        //TODO hacer que se carguen los datos (controlar también que estén vacíos)
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
}
