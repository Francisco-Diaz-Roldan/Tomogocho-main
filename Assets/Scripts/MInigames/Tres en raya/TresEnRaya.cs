using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TresEnRaya : MonoBehaviour
{
    [SerializeField] private TMP_Text _turnText;
    [SerializeField] private GameObject _panelResultado;
    [SerializeField] private TMP_Text _resultadosTotalesText;
    public Button[] buttons;
    public Image[] buttonImages;
    public Sprite xSprite;
    public Sprite oSprite;
    private Sprite currentSprite;

    private string playerTurn = "Jugador";
    private int _partidasGanadasJugador = 0;
    private int _partidasGanadasTomogocho = 0;
    private int _empates = 0;
    private bool gameOver = false;

    private const string PartidasGanadasJugadorKey = "PartidasGanadasJugador";
    private const string PartidasGanadasTomogochoKey = "PartidasGanadasTomogocho";
    private const string EmpatesKey = "Empates";

    void Start()
    {
        currentSprite = xSprite;
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
        if (gameOver)
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
        if (!gameOver && buttonImages[index].sprite == null)
        {
            buttonImages[index].sprite = currentSprite;
            if (CheckWin())
            {
                if (playerTurn == "Jugador")
                {
                    IncrementarPartidasGanadasJugador();
                    _turnText.text = "¡Enhorabuena, has ganado!";
                }
                else
                {
                    IncrementarPartidasGanadasTomogocho();
                    _turnText.text = "Lo siento, has perdido.";
                }
                SaveResultadosTotales();
                ShowResultPanel();
            }
            else if (CheckDraw())
            {
                IncrementarEmpates();
                _turnText.text = "Vaya sorpresa, ha habido un empate.";
                SaveResultadosTotales();
                ShowResultPanel();
            }
            else
            {
                SwitchTurn();
                if (playerTurn == "Tomogocho")
                {
                    StartCoroutine(AIMove());
                }
                UpdateTurnText();
            }
        }
    }

    

    IEnumerator AIMove()
    {
        yield return new WaitForSeconds(0.75f); // Espera 0.75 segundos antes de hacer el movimiento
        int index = GetAIMove();
        if (index != -1)
        {
            buttonImages[index].sprite = currentSprite;
            if (CheckWin())
            {
                ResetBoard();
                SaveResultadosTotales();
                ShowResultPanel();
            }
            else
            {
                SwitchTurn();
                UpdateTurnText();
            }
        }
    }

    int GetAIMove()
    {
        // Prioridad 1: Ganar si es posible
        int winMove = FindWinningMove(currentSprite);
        if (winMove != -1) return winMove;

        // Prioridad 2: Bloquear al jugador si está a punto de ganar
        Sprite opponentSprite = (currentSprite == xSprite) ? oSprite : xSprite;
        int blockMove = FindWinningMove(opponentSprite);
        if (blockMove != -1) return blockMove;

        // Prioridad 3: Hacer el mejor movimiento posible
        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttonImages[i].sprite == null)
            {
                return i;
            }
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
            currentSprite = oSprite;
        }
        else
        {
            playerTurn = "Jugador";
            currentSprite = xSprite;
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
        currentSprite = xSprite;
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
    }

    bool CheckDraw()
    {
        // Verificar si todas las casillas están ocupadas
        for (int i = 0; i < buttonImages.Length; i++)
        {
            if (buttonImages[i].sprite == null)
            {
                // Aún hay casillas vacías, no hay empate
                return false;
            }
        }

        // Si se recorre todo el tablero y no hay casillas vacías, es un empate
        return true;
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
}