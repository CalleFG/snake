using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private Text resultText;
    [SerializeField] private Text movesText;

    public void DisplayPanel(bool isWin, int moveCount)
    {
        if(isWin) SetWinText();
        else SetLoseText();
        
        SetMovesText(moveCount);

        gameObject.SetActive(true);
    }

    private void SetWinText()
    {
        resultText.text = $"You win!";
    }

    private void SetLoseText()
    {
        resultText.text = $"You lose!";
    }

    private void SetMovesText(int moves)
    {
        movesText.text = moves.ToString();
    }
}
