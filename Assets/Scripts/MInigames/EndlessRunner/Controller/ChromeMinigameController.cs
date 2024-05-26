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
    [SerializeField] SkySpawner _skySpawner; 
    [SerializeField] SkySpawner _skySpawner2;
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
    }

    public void Pause()
    {
        if (_isMiniGameActive)
        {
            _isMiniGameActive = false ;
            _panelMiniGame.SetActive(false);
        }
        Time.timeScale = 0f;
        _panelHome.SetActive(true);
        _isHomeMenuActive = true;
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
            Time.timeScale = 1f;
        }
        _isHomeMenuActive = false;
    }

    public void OpenPanelMiniGame()
    {
        if (_isHomeMenuActive)
        {
            _panelHome.SetActive(false);
            _isHomeMenuActive = false;
        }
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
      if ( !_isMiniGameActive)
        {
            OpenPanelMiniGame();
        }
      else
        {
            ClosePanelMiniGame(); 
        }
    }

    public void ToggleHomeMenu()
    {
        if (_isHomeMenuActive)
        {
            GoBack();
            if (_panelMiniGame.activeSelf) ClosePanelMiniGame();
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
        _skySpawner.StartSpawning();
        _skySpawner2.StartSpawning();
        _scoreManager.StartGame();
        _player.StartGame();
    }

    private void StopGame()
    {
        _obstacleSpawner.StopSpawning();
        _skySpawner.StopSpawning();
        _skySpawner2.StopSpawning();
        _scoreManager.StopGame();
        _player.StopGame();
    }
}