using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;    // Namespace for TextMeshPro functionality

// This script displays the current level number on a UI panel in the game.
public class currentLevel : MonoBehaviour
{
    public GameObject panel;

    // Reference to the TextMeshProUGUI component to display the level number
    public TextMeshProUGUI levelText;
    int lvl;    // Variable to store the current level number

    // Start is called before the first frame update
    void Start()
    {
        // Get the index of the currently active scene
        lvl = SceneManager.GetActiveScene().buildIndex;

        // Adjust the level number by subtracting 1 (e.g., if levels start from 1 instead of 0)
        lvl = lvl - 1;

        // Convert the level number to a string and display it on the UI
        levelText.text = lvl.ToString();
    }
}
