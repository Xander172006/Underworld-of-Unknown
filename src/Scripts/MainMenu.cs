using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject healthBarCanvas;

    public void PlayGame()
    {
        // If you are using scenes, use this:
        Debug.Log("Play button pressed!");
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}