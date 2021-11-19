using UnityEngine;
using UnityEngine.SceneManagement;

public class CGameManager : MonoBehaviour
{
    [SerializeField] private WinLoseUI winLoseUI;

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    private void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Win(int moveCount)
    {
        winLoseUI.DisplayPanel(true, moveCount);
        PauseGame();
    }

    public void Lose(int moveCount)
    {
        winLoseUI.DisplayPanel(false, moveCount);
        PauseGame();
    }
}
