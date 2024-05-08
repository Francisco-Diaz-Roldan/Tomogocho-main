using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject _panelMenu;

    public void GoToMenu()
    {
        _panelMenu.SetActive(true);
    }

    public void GoToGameScene()
    {
        // Para ir a la escena de Juego
        SceneManager.LoadScene("GameScene");
    }

    /* public void QuitGame()
     {
         // Para salir de la app
         Application.Quit();
     }*/
}
