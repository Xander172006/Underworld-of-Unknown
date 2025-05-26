using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;  
 
    // --- Set timer to 5 minutes (300 seconds) ---
    public float timeRemaining = 300f; 
    private bool timerIsRunning = true;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // -- Update the timer every frame --
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                // -- Stop the timer when it reaches zero --
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        // -- Calculate minutes and seconds from the remaining time --
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // -- Format the timer text to display MM:SS --
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}