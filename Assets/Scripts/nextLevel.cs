using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextLevel : MonoBehaviour
{
    public GameObject panel; // A reference to the panel that might be used for UI

    // Function to load the next level
    public void nextLvl()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex; // Get the index of the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene in the build order
    }
}
