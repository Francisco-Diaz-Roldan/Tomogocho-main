using UnityEngine;
using UnityEngine.SceneManagement;

public class ChromeMinigameController : MonoBehaviour
{

    [SerializeField] GameObject _panelJugar;
    [SerializeField] GameObject _panelHome;
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelResetMenu;
    [SerializeField] ObstacleSpawner _obstacleSpawner;
    [SerializeField] Score _scoreManager;
    [SerializeField] PlayerChrome _player;

    public void Restart()
    {
        Time.timeScale = 0f;
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
        _obstacleSpawner.StartSpawning();
        _scoreManager.StartGame();
        _player.StartGame();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _panelHome.SetActive(true);
         _panelJugar.SetActive(false) ;
        _obstacleSpawner.StopSpawning();
        _scoreManager.StopGame();
        _player.StopGame();
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
            _obstacleSpawner.StartSpawning();
            _scoreManager.StartGame();
            _player.StartGame();
        }
    }
}
