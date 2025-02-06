using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextLevel : MonoBehaviour
{



    public GameObject panel;

    public void nextLvl()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;


        /*
        if(currentLevel == 2)
        {
            panel.transform.Find("Level1").GetComponent<Image>().color = Color.green;
           
        }
        else if (currentLevel == 3)
        {
            panel.transform.Find("Level2").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 4)
        {
            panel.transform.Find("Level3").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 5)
        {
            panel.transform.Find("Level4").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 6)
        {
            panel.transform.Find("Level5").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 7)
        {
            panel.transform.Find("Level6").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 8)
        {
            panel.transform.Find("Level7").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 9)
        {
            panel.transform.Find("Level8").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 10)
        {
            panel.transform.Find("Level9").GetComponent<Image>().color = Color.green;
        }
        else if (currentLevel == 11)
        {
            panel.transform.Find("Level10").GetComponent<Image>().color = Color.green;
        }
        */
    

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // se incarca urmatoarea scena din stiva de scene

    }

   

}
