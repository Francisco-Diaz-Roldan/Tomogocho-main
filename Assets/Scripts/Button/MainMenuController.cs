using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private PlayerData _playerData;

    public void GoToMenu()
    {
        _panelMenu.SetActive(true);
    }

    public void ResetGame()
    {
        _playerData.ResetValues(true); // Resetear valores sin iniciar un nuevo juego
        GoToGameScene();
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");// Para ir a la escena de Juego
    }   
}
