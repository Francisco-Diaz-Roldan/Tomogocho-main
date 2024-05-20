using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TresEnRaya : MonoBehaviour
{
    public Button[] buttons;
    public Image[] buttonImages;
    public Sprite xSprite;
    public Sprite oSprite;
    private Sprite currentSprite;
    [SerializeField] private TMP_Text _tunrText;
    private string playerTurn = "Jugador";


    void Start()
    {
        currentSprite = xSprite;
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
        UpdateTurnText();
    }

    void UpdateTurnText()
    {
        if (playerTurn == "Jugador")
        {
            _tunrText.text = "Tu turno";
        }
        else
        {
            _tunrText.text = "Turno del Tomogocho";
        }
    }


    void OnButtonClick(int index)
    {
        if (buttonImages[index].sprite == null)
        {
            buttonImages[index].sprite = currentSprite;
            if (CheckWin())
            {
                Debug.Log(playerTurn + " ha ganado!");
                ResetBoard();
            }
            else
            {
                SwitchTurn();
                if (playerTurn == "Tomogocho")
                {
                    StartCoroutine(AIMove());
                }
            }
        }
    }

    IEnumerator AIMove()
    {
        yield return new WaitForSeconds(0.8f); // Espera 0.8 segundos antes de hacer el movimiento
        int index = GetAIMove();
        if (index != -1)
        {
            buttonImages[index].sprite = currentSprite;
            if (CheckWin())
            {
                Debug.Log(playerTurn + " ha ganado!");
                ResetBoard();
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

        // Prioridad 2: Bloquear al jugador si est� a punto de ganar
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
        return -1; // No hay movimientos disponibles
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
}
