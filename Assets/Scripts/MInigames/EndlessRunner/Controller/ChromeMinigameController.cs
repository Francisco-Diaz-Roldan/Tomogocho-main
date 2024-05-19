using UnityEngine;
using UnityEngine.SceneManagement;

public class ChromeMinigameController : MonoBehaviour
{

    [SerializeField] GameObject _panelJugar;
    [SerializeField] GameObject _panelHome;
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelResetMenu;

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ChromeMinigameScene");
    }

    public void GoToGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void Play()
    {
        _panelJugar.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _panelHome.SetActive(true);
    }

    public void OpenResetMenu()
    {
        _panelResetMenu.SetActive(true);
    }

    public void CloseResetMenu()
    {
        _panelResetMenu.SetActive(false);
    }

    public void GoToMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void GoBack()
    {
        _panelHome.SetActive(false);
        if (!_panelGameOver.activeSelf) 
        {
            Time.timeScale = 1f;
        }
    }
}
