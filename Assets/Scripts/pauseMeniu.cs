using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMeniu : MonoBehaviour
{
    //public static bool GameisPaused = false;  // Unused in this version, could be used for pause state

    public GameObject secondary_canvas; // Reference to the pause menu canvas
    public GameObject primary_canvas; // Reference to the main level canvas

    // Function for the Resume button
    public void Resume()
    {
        secondary_canvas.SetActive(false); // Deactivates the pause menu
        primary_canvas.SetActive(true); // Activates the main level canvas, resuming gameplay
    }

    // Function for the Menu button
    public void LoadMenu()
    {
        // Get the current level index and check if it's greater than or equal to the last saved level
        int lastLevel = SceneManager.GetActiveScene().buildIndex;

        // If the current level index is greater than the last saved level, update it
        if (lastLevel >= PlayerPrefs.GetInt("lastLevel")) 
        {
            PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex-1);
        }

        // Load the "Menu" scene
        SceneManager.LoadScene("Menu"); 
    }

    // Function for the Quit Game button
    public void QuitGame()
    {
        // Quit the application when the player clicks the quit button
        Application.Quit(); 
    }
}
