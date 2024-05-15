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
        if (_playerData.HasDied()) // En caso de que la criatura tenga 0 de vida al darle a jugar se resetean sus valores
        {
            _playerData.ResetValues(true);
        }
        SceneManager.LoadScene("GameScene");// Para ir a la escena de Juego
    }   
}
