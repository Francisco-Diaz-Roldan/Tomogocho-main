using UnityEngine;
using UnityEngine.SceneManagement;

public class ChromeMinigameController : MonoBehaviour
{

    [SerializeField] GameObject _panelJugar;

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

    public void Jugar()
    {
        _panelJugar.SetActive(false);
        Time.timeScale = 1f;
    }
}
