using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // --- Alternate between healthbar being shown or not --- -- is Outdated --
    public GameObject mainMenuCanvas;
    public GameObject healthBarCanvas;

    public void PlayGame()
    {
        // -- Load the first scene after MainMeny (game) --
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        // -- Quit the game application --
        Application.Quit();
    }
}