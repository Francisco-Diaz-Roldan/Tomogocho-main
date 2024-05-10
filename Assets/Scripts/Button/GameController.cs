using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _botonHome;
    [SerializeField] private GameObject _panelHome;
    private PlayerDead _playerDead;

    private void Start()
    {
        _playerDead = FindObjectOfType<PlayerDead>();
        if (_playerDead == null) { return; }
    }
    public void Pausa()
    {
        if (!_playerDead.IsDead)
        {
            Time.timeScale = 0f;
            _panelHome.SetActive(true);//_panelHome es el menú de pausa
        }
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        _panelHome.SetActive(false);//_panelHome es el menú de pausa
    }

    public void GoToMainScene()
    {
        // Para ir a la escena Principal
        //TODO hacer que se almacenen los datos 
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
