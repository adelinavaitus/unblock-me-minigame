using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles returning to the main menu in a Unity project.
public class backToMenu : MonoBehaviour
{
    // This method is triggered when the "Back to Menu" button is pressed.
    public void BacktoMenuButton()
    {
        // Loads the scene at index 0, the main menu scene.
        SceneManager.LoadScene(0);
    }
}
