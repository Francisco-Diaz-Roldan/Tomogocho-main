using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void QuitGame()
    {
        // Para salir de la app
        Application.Quit();
    }

    public void GoToGameScene()
    {
        // Para ir a la escena de Juego
        SceneManager.LoadScene("GameScene");
    }
}
