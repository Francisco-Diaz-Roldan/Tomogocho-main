using UnityEngine;
using UnityEngine.SceneManagement;

public class ChromeMinigameController : MonoBehaviour
{
    [SerializeField] GameObject _panelJugar;
    [SerializeField] GameObject _panelHome;
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelResetMenu;
    [SerializeField] GameObject _panelMiniGame;
    [SerializeField] ObstacleSpawner _obstacleSpawner;
    [SerializeField] ObstacleSpawner _cloudSpawner1;
    [SerializeField] ObstacleSpawner _cloudSpawner2;
    [SerializeField] Score _scoreManager;
    [SerializeField] PlayerChrome _player;
    private bool _isHomeMenuActive = false;
    private bool _isMiniGameActive = false;

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
        StartGame();
        /*Time.timeScale = 1f;
        _obstacleSpawner.StartSpawning();
        _cloudSpawner1.StartSpawning();
        _cloudSpawner2.StartSpawning();
        _scoreManager.StartGame();
        _player.StartGame();*/
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _panelHome.SetActive(true);
        _isHomeMenuActive = true;
        //_panelJugar.SetActive(false); ->Eliminar esta linea
        StopGame();
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
            _isHomeMenuActive = false;
            Time.timeScale = 1f;
        }
    }

    public void OpenPanelMiniGame()
    {
        _panelMiniGame.SetActive(true);
        _isMiniGameActive = true;
    }

    public void ClosePanelMiniGame()
    {
        _panelMiniGame.SetActive(false);
        _isMiniGameActive = false;
    }

    public void ToggleMiniGameButton()
    {
        _isMiniGameActive = !_isMiniGameActive;
        _panelMiniGame.SetActive(_isMiniGameActive);
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

    private void StartGame()
    {
        Time.timeScale = 1f;
        _obstacleSpawner.StartSpawning();
        _cloudSpawner1.StartSpawning();
        _cloudSpawner2.StartSpawning();
        _scoreManager.StartGame();
        _player.StartGame();
    }

    private void StopGame()
    {
        _obstacleSpawner.StopSpawning();
        _cloudSpawner1.StopSpawning();
        _cloudSpawner2.StopSpawning();
        _scoreManager.StopGame();
        _player.StopGame();
    }
}