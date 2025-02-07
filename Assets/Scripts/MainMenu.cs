using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject panel;
    TextMeshProUGUI levelText;

    // Method to start the game. It loads the next scene in the build index.
    public void PlayGame()
    {
        // Load the next scene based on the current scene's build index.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to quit the game. Works only in a built application (not in the Unity Editor).
    public void QuitGame ()
    {
        Application.Quit(); // Quits the application.
    }
}
