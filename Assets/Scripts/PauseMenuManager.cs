using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);

        //unpause the game if coming from the main menu
        Time.timeScale = 1f; 
    }

    private void Update()
    {
        // Check for the Escape key to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;

        // Activate or deactivate the pause menu panel
        pauseMenuPanel.SetActive(isPaused);

        // Pause or resume the game time
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        TogglePauseMenu();
    }

    public void MainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void PauseGameFromButton()
    {
        TogglePauseMenu();
    }
}
