using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    // -- Reference to the UI element for winning --
    public GameObject gameWonUI; 
    private bool gameIsWon = false;

    public void CheckIfGameWon(int remainingEnemies)
    {
        // -- the game is won if all enemies are defeated --
        if (!gameIsWon && remainingEnemies <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        // -- show the "You Win!" UI and pause the game --
        gameIsWon = true;
        if (gameWonUI != null)
            gameWonUI.SetActive(true);

        Time.timeScale = 0f;
    }

    // --- restart button if the player has won --
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}