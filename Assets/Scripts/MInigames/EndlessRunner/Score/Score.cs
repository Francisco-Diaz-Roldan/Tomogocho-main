using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    private int _score;
    private float _timer;
    private int _highScore;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highScoreText.text = string.Format("{0:00000}", _highScore);
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        int scorePerSeconds = 10;
        _timer += Time.deltaTime;
        _score = (int)(_timer * scorePerSeconds);
        _score = Mathf.RoundToInt(_score / 10) * 10;
        _scoreText.text = string.Format("{0:00000}", _score);

        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            PlayerPrefs.Save();
            _highScoreText.text = string.Format("{0:00000}", _highScore);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();

        _score = 0;
        _highScore = 0;
        _scoreText.text = string.Format("{0:00000}", _score);
        _highScoreText.text = string.Format("{0:00000}", _highScore);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        _score = 0;
        _timer = 0;

        _scoreText.text = string.Format("{0:00000}", _score);
        _highScoreText.text = string.Format("{0:00000}", _highScore);
    }
}
