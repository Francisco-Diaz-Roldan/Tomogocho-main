using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiedraPapelTijerasGameController : MonoBehaviour
{
    [SerializeField] private GameObject _panelResultado;

    public void Restart()
    {
        SceneManager.LoadScene("PiedraPapelTijerasMinigameScene");
    }

    public void OpenPanelResultados()
    {
        _panelResultado.SetActive(true);
    }
}
