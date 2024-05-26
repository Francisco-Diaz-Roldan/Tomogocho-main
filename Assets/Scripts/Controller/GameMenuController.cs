using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private PlayerData _playerData;

    public void GoToMenu()
    {
        _panelMenu.SetActive(true);
    }

    public void GoBack()
    {
        _panelMenu.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void StartNewGame()
    {
        ControlGame();
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
