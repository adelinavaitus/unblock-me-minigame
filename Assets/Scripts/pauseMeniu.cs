using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMeniu : MonoBehaviour
{
    //public static bool GameisPaused = false;

    public GameObject secondary_canvas;
    public GameObject primary_canvas;



    //functie pentru buton Resume
    public void Resume()
    {
        secondary_canvas.SetActive(false); // se dezactiveaza meniul de pauza
        primary_canvas.SetActive(true); // se activeaza canvasul principal din level
    }

    //functie pentru butonul Menu
    public void LoadMenu()
    {
        //PlayerPrefs.SetInt("lastLevel", 2 );
        int lastLevel = SceneManager.GetActiveScene().buildIndex;
        if (lastLevel >= PlayerPrefs.GetInt("lastLevel")) 
        {
            PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex-1);
        }
        SceneManager.LoadScene("Menu"); // se incarca scena cu nivelul principal

    }

    //functie pentru buton Quit Game
    public void QuitGame()
    {
        //Debug.Log("Quit Game");
        Application.Quit(); // se inchide aplicatia
    }

}
