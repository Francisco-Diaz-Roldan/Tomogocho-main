using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelMenu;
    [SerializeField] private GameObject _panelRecord;
    [SerializeField] private PlayerData _playerData;

    public void GoToMenu()
    {
        _panelMenu.SetActive(true);
    }

    public void ResetGame()
    {
        _playerData.ResetValues(true);
        GoToGameScene();
    }

    public void GoToGameScene()
    {
        if (_playerData.HasDied())
        {
            _playerData.ResetValues(true);
        }
        SceneManager.LoadScene("GameScene");
    }

    public void OpenRecordTomogochoAge()
    {
        _panelRecord.SetActive(true);
    }

    public void CloseRecordTomogochoAge()
    {
        _panelRecord.SetActive(false);
    }
}