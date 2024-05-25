using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TresEnRaya : MonoBehaviour
{
    [SerializeField] private TMP_Text _turnText;
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private TMP_Text _resultadosTotalesText;
    public Button[] buttons;
    public Image[] buttonImages;
    public Sprite _playerSprite;
    public Sprite _tomogochoSprite;
    private Sprite currentSprite;
    public bool partidaTerminada = false;

    private string playerTurn = "Jugador";
    private int _partidasGanadasJugador = 0;
    private int _partidasGanadasTomogocho = 0;
    private int _empates = 0;
    private bool _gameOver = false;

    private const string PartidasGanadasJugadorKey = "PartidasGanadasJugador";
    private const string PartidasGanadasTomogochoKey = "PartidasGanadasTomogocho";
    private const string EmpatesKey = "Empates";

    void Start()
    {
        currentSprite = _playerSprite;
        LoadResultadosTotales();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
        UpdateTurnText();
    }

    void UpdateTurnText()
    {
        if (_gameOver)
            return;

        if (playerTurn == "Jugador")
        {
            _turnText.text = "Tu turno";
        }
        else
        {
            _turnText.text = "Turno del Tomogocho";
        }
    }

    void OnButtonClick(int index)
    {
        if (!_gameOver && buttonImages[index].sprite == null)
        {
            buttonImages[index].sprite = currentSprite;
            SetSpritesVisibles(index);

            if (CheckWin())
            {
                HandleWin();
            }
            else if (CheckDraw())
            {
                HandleDraw();
            }
            else
            {
                SwitchTurn();
                if (playerTurn == "Tomogocho")
                {
                    DisableButtons();
                    StartCoroutine(AIMove());
                }
                UpdateTurnText();
            }
        }
    }

    void HandleWin()
    {
        if (playerTurn == "Jugador")
        {
            IncrementarPartidasGanadasJugador();
            _turnText.text = "�Enhorabuena, has ganado!";
        }
        SaveResultadosTotales();
        ShowResultPanel();
        partidaTerminada = true;
    }

    void HandleDraw()
    {
        IncrementarEmpates();
        _turnText.text = "Vaya sorpresa, ha habido un empate.";
        SaveResultadosTotales();
        ShowResultPanel();
        partidaTerminada = true;
    }
    IEnumerator AIMove()
    {
        yield return new WaitForSeconds(0.75f); // Espera 0.75 segundos antes de hacer el movimiento
        int index = GetAIMove();
        if (index != -1)
        {
            buttonImages[index].sprite = currentSprite;
            SetSpritesVisibles(index);
            if (CheckWin())
            {
                IncrementarPartidasGanadasTomogocho(); //Aqu� es donde se a�aden las partidas ganadas por el Tomogocho
                _turnText.text = "Lo siento, has perdido contra el todopoderoso Tomogocho.";
                ResetBoard();
                SaveResultadosTotales();
                ShowResultPanel();
                partidaTerminada = true;
            }
            else
            {
                SwitchTurn();
                UpdateTurnText();
            }
        }
        EnableButtons();
    }

    void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage.sprite != _playerSprite && buttonImage.sprite != _tomogochoSprite)
            {
                button.interactable = true;
            }
        }
    }

    int GetAIMove()
    {
        // Prioridad 1: Ganar si es posible
        int winMove = FindWinningMove(currentSprite);
        if (winMove != -1) return winMove;

        // Prioridad 2: Bloquear al jugador si est� a punto de ganar
        Sprite opponentSprite = (currentSprite == _playerSprite) ? _tomogochoSprite : _playerSprite;
        int blockMove = FindWinningMove(opponentSprite);
        if (blockMove != -1) return blockMove;

        // Prioridad 3: Elegir la posici�n central si est� disponible
        int centerIndex = 4;
        if (buttonImages[centerIndex].sprite == null) return centerIndex;

        // Prioridad 4: Elegir una posici�n aleatoria entre las disponibles
        List<int> availablePositions = new List<int>();
        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttonImages[i].sprite == null)
            {
                availablePositions.Add(i);
            }
        }
        if (availablePositions.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
            return availablePositions[randomIndex];
        }
        return -1; // No quedan movimientos disponibles
    }


    int FindWinningMove(Sprite sprite)
    {
        int[,] winCombinations = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Horizontales
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Verticales
            {0, 4, 8}, {2, 4, 6}             // Diagonales
        };

        for (int i = 0; i < winCombinations.GetLength(0); i++)
        {
            int a = winCombinations[i, 0];
            int b = winCombinations[i, 1];
            int c = winCombinations[i, 2];

            if (buttonImages[a].sprite == sprite && buttonImages[b].sprite == sprite && buttonImages[c].sprite == null)
            {
                return c;
            }
            if (buttonImages[a].sprite == sprite && buttonImages[c].sprite == sprite && buttonImages[b].sprite == null)
            {
                return b;
            }
            if (buttonImages[b].sprite == sprite && buttonImages[c].sprite == sprite && buttonImages[a].sprite == null)
            {
                return a;
            }
        }
        return -1;
    }

    void SwitchTurn()
    {
        if (playerTurn == "Jugador")
        {
            playerTurn = "Tomogocho";
            currentSprite = _tomogochoSprite;
        }
        else
        {
            playerTurn = "Jugador";
            currentSprite = _playerSprite;
        }
        UpdateTurnText();
    }

    bool CheckWin()
    {
        int[,] winCombinations = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Horizontales
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Verticales
            {0, 4, 8}, {2, 4, 6}             // Diagonales
        };

        for (int i = 0; i < winCombinations.GetLength(0); i++)
        {
            int a = winCombinations[i, 0];
            int b = winCombinations[i, 1];
            int c = winCombinations[i, 2];

            if (buttonImages[a].sprite != null &&
                buttonImages[a].sprite == buttonImages[b].sprite &&
                buttonImages[a].sprite == buttonImages[c].sprite)
            {
                return true;
            }
        }
        return false;
    }

    void ResetBoard()
    {
        for (int i = 0; i < buttonImages.Length; i++)
        {
            buttonImages[i].sprite = null;
        }
        playerTurn = "Jugador";
        currentSprite = _playerSprite;
        EnableButtons();  
    }

    void ShowResultPanel()
    {
        _panelResultado.SetActive(true);
        _resultadosTotalesText.text = "Partidas ganadas: " + _partidasGanadasJugador +
                             "\nPartidas perdidas: " + _partidasGanadasTomogocho +
                             "\nEmpates: " + _empates;
    }

    void SaveResultadosTotales()
    {
        PlayerPrefs.SetInt(PartidasGanadasJugadorKey, _partidasGanadasJugador);
        PlayerPrefs.SetInt(PartidasGanadasTomogochoKey, _partidasGanadasTomogocho);
        PlayerPrefs.SetInt(EmpatesKey, _empates);
        PlayerPrefs.Save();
    }

    void LoadResultadosTotales()
    {
        _partidasGanadasJugador = PlayerPrefs.GetInt(PartidasGanadasJugadorKey, 0);
        _partidasGanadasTomogocho = PlayerPrefs.GetInt(PartidasGanadasTomogochoKey, 0);
        _empates = PlayerPrefs.GetInt(EmpatesKey, 0);
        UpdateResultadosTotales();
    }
    bool CheckDraw()
    {
        // Comprueba que todas las casillas est�n ocupadas y no haya un ganador ni un perdedor
        if (!CheckWin())
        {
            for (int i = 0; i < buttonImages.Length; i++)
            {
                if (buttonImages[i].sprite == null)
                {
                    // A�n hay casillas vac�as, no hay empate
                    return false;
                }
            }

            // Si se recorre todo el tablero y no hay casillas vac�as, es un empate
            return true;
        }
        return false; // No es un empate si hay un ganador o un perdedor
    }

    void IncrementarPartidasGanadasJugador()
    {
        _partidasGanadasJugador++;
    }

    void IncrementarPartidasGanadasTomogocho()
    {
        _partidasGanadasTomogocho++;
    }

    void IncrementarEmpates()
    {
        _empates++;
    }

    public void ResetData()
    {
        _partidasGanadasJugador = 0;
        _partidasGanadasTomogocho = 0;
        _empates = 0;
        SaveResultadosTotales();
        UpdateResultadosTotales();
        ResetBoard();
        _panelResultado.SetActive(false);
    }

    void UpdateResultadosTotales()
    {
        _resultadosTotalesText.text = "Partidas ganadas: " + _partidasGanadasJugador +
                                      "\nPartidas perdidas: " + _partidasGanadasTomogocho +
                                      "\nEmpates: " + _empates;
    }

    void SetSpritesVisibles(int index)
    {
        if (index >= 0 && index < buttonImages.Length && buttonImages[index] != null)
        {
            // Obt�n el color actual del bot�n
            Color buttonColor = buttonImages[index].color;

            // Establece el canal alfa (A) del color
            buttonColor.a = 1f; // Hacer el bot�n completamente opaco

            // Asigna el nuevo color al bot�n
            buttonImages[index].color = buttonColor;
        }
    }
}