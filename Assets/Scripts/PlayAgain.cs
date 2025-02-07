using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    // Function for the Play Again button
    public void PlayAgainButton()
    {
        // Resets the "lastLevel" to 1, which could represent the first level
        PlayerPrefs.SetInt("lastLevel", 1);

        // Loads the scene at index 1, which is the starting level (Level 1)
        SceneManager.LoadScene(1);
    }
}
