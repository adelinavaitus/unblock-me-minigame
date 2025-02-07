using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    // Function to reset the current level
    public void reset()
    {
        // Reloads the current active scene by getting its build index and loading it again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
