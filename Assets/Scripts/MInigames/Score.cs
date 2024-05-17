using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _score;
    private float _timer;

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        int scorePerSeconds = 10;
        _timer += Time.deltaTime;
        _score = (int)(_timer  * scorePerSeconds);
        _score = Mathf.RoundToInt(_score / 10) * 10;
        _scoreText.text = string.Format("{0:00000}", _score);
    }
}
