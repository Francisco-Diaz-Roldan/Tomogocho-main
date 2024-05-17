using UnityEngine;
using UnityEngine.SceneManagement;

public class ChromeMinigameController : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("ChromeMinigameScene");
        Time.timeScale = 1f;
    }
}
