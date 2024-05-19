using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TresEnRaya : MonoBehaviour
{
    public Button[] buttons;
    public Image[] buttonImages;
    public Sprite xSprite;
    public Sprite oSprite;
    private Sprite currentSprite;
    private string playerTurn = "X";

    void Start()
    {
        currentSprite = xSprite;
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    void OnButtonClick(int index)
    {
        if (buttonImages[index].sprite == null)
        {
            buttonImages[index].sprite = currentSprite;
            if (CheckWin())
            {
                Debug.Log(playerTurn + " wins!");
                ResetBoard();
            }
            else
            {
                if (playerTurn == "X")
                {
                    playerTurn = "O";
                    currentSprite = oSprite;
                }
                else
                {
                    playerTurn = "X";
                    currentSprite = xSprite;
                }
            }
        }
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
        playerTurn = "X";
        currentSprite = xSprite;
    }
}
