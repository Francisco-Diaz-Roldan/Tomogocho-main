using UnityEngine;
using UnityEngine.SceneManagement;

public class ChromeMinigameController : MonoBehaviour
{

    [SerializeField] GameObject _panelJugar;
    [SerializeField] GameObject _panelHome;
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelResetMenu;
    [SerializeField] ObstacleSpawner _obstacleSpawner;
    [SerializeField] ObstacleSpawner _cloudSpawner1;
    [SerializeField] ObstacleSpawner _cloudSpawner2;
    [SerializeField] Score _scoreManager;
    [SerializeField] PlayerChrome _player;
    private bool _isHomeMenuActive = false;



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
        _cloudSpawner1.StartSpawning();
        _cloudSpawner2.StartSpawning();
        _scoreManager.StartGame();
        _player.StartGame();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _panelHome.SetActive(true);
        _isHomeMenuActive = true;
        _panelJugar.SetActive(false) ;
        _obstacleSpawner.StopSpawning();
        _cloudSpawner1.StopSpawning();
        _cloudSpawner2.StopSpawning();
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
            _isHomeMenuActive = false;
            _obstacleSpawner.StartSpawning();
            _cloudSpawner1.StartSpawning();
            _cloudSpawner2.StartSpawning();
            _scoreManager.StartGame();
            _player.StartGame();
        }
    }

    public void ToggleHomeMenu()
    {
        if (_isHomeMenuActive)
        {
            GoBack();
        }
        else
        {
            Pause();
        }
    }
}