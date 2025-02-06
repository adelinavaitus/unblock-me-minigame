using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Levels : MonoBehaviour
{
    public Button button1_yellow;
    public Button button1_green;

    public Button button2_yellow;
    public Button button2_red;
    public Button button2_green;

    public Button button3_green;
    public Button button3_red;
    public Button button3_yellow;

    public Button button4_red;
    public Button button4_yellow;
    public Button button4_green;

    public Button button5_red;
    public Button button5_yellow;
    public Button button5_green;

    public Button button6_red;
    public Button button6_yellow;
    public Button button6_green;

    public Button button7_red;
    public Button button7_yellow;
    public Button button7_green;

    public Button button8_red;
    public Button button8_yellow;
    public Button button8_green;

    public Button button9_red;
    public Button button9_yellow;
    public Button button9_green;

    public Button button10_red;
    public Button button10_yellow;
    public Button button10_green;

    public GameObject panel;
    public TextMeshProUGUI levelText;




    public void Start()
    {
        // if (levelText.color == Color.green)
        levelText.text = PlayerPrefs.GetInt("lastLevel").ToString();
        //else
        // levelText.text = "Salut";
    }



     public void Update()
     {

        if (PlayerPrefs.GetInt("lastLevel") < 2)
        {
            button1_yellow.gameObject.SetActive(true);
            button2_red.gameObject.SetActive(true);
            button3_red.gameObject.SetActive(true);
            button4_red.gameObject.SetActive(true);
            button5_red.gameObject.SetActive(true);
            button6_red.gameObject.SetActive(true);
            button7_red.gameObject.SetActive(true);
            button8_red.gameObject.SetActive(true);
            button9_red.gameObject.SetActive(true);
            button10_red.gameObject.SetActive(true);

            button1_green.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);

            button2_yellow.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(false);
            button6_yellow.gameObject.SetActive(false);
            button7_yellow.gameObject.SetActive(false);
            button8_yellow.gameObject.SetActive(false);
            button9_yellow.gameObject.SetActive(false);
            button10_yellow.gameObject.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("lastLevel") == 2)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(true);
            button2_green.gameObject.SetActive(false);

            button3_red.gameObject.SetActive(true);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(false);

            button4_red.gameObject.SetActive(true);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(false);

            button5_red.gameObject.SetActive(true);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(false);

            button6_red.gameObject.SetActive(true);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(false);

            button7_red.gameObject.SetActive(true);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(false);

            button8_red.gameObject.SetActive(true);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }


        else if (PlayerPrefs.GetInt("lastLevel") == 3)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(true);
            button3_green.gameObject.SetActive(false);

            button4_red.gameObject.SetActive(true);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(false);

            button5_red.gameObject.SetActive(true);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(false);

            button6_red.gameObject.SetActive(true);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(false);

            button7_red.gameObject.SetActive(true);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(false);

            button8_red.gameObject.SetActive(true);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("lastLevel") == 4)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(true);
            button4_green.gameObject.SetActive(false);

            button5_red.gameObject.SetActive(true);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(false);

            button6_red.gameObject.SetActive(true);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(false);

            button7_red.gameObject.SetActive(true);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(false);

            button8_red.gameObject.SetActive(true);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);

        }
        else if (PlayerPrefs.GetInt("lastLevel") == 5)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(true);

            button5_red.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(true);
            button5_green.gameObject.SetActive(false);

            button6_red.gameObject.SetActive(true);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(false);

            button7_red.gameObject.SetActive(true);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(false);

            button8_red.gameObject.SetActive(true);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("lastLevel") == 6)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(true);

            button5_red.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(true);

            button6_red.gameObject.SetActive(false);
            button6_yellow.gameObject.SetActive(true);
            button6_green.gameObject.SetActive(false);

            button7_red.gameObject.SetActive(true);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(false);

            button8_red.gameObject.SetActive(true);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }

        else if (PlayerPrefs.GetInt("lastLevel") == 7)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(true);

            button5_red.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(true);

            button6_red.gameObject.SetActive(false);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(true);

            button7_red.gameObject.SetActive(false);
            button7_yellow.gameObject.SetActive(true);
            button7_green.gameObject.SetActive(false);

            button8_red.gameObject.SetActive(true);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("lastLevel") == 8)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(true);

            button5_red.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(true);

            button6_red.gameObject.SetActive(false);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(true);

            button7_red.gameObject.SetActive(false);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(true);

            button8_red.gameObject.SetActive(false);
            button8_yellow.gameObject.SetActive(true);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(true);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("lastLevel") == 9)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(true);

            button5_red.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(true);

            button6_red.gameObject.SetActive(false);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(true);

            button7_red.gameObject.SetActive(false);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(true);

            button8_red.gameObject.SetActive(false);
            button8_yellow.gameObject.SetActive(true);
            button8_green.gameObject.SetActive(false);

            button9_red.gameObject.SetActive(false);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(false);

            button10_red.gameObject.SetActive(true);
            button10_yellow.gameObject.SetActive(false);
            button10_green.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("lastLevel") == 10)
        {
            button1_yellow.gameObject.SetActive(false);
            button1_green.gameObject.SetActive(true);

            button2_red.gameObject.SetActive(false);
            button2_yellow.gameObject.SetActive(false);
            button2_green.gameObject.SetActive(true);

            button3_red.gameObject.SetActive(false);
            button3_yellow.gameObject.SetActive(false);
            button3_green.gameObject.SetActive(true);

            button4_red.gameObject.SetActive(false);
            button4_yellow.gameObject.SetActive(false);
            button4_green.gameObject.SetActive(true);

            button5_red.gameObject.SetActive(false);
            button5_yellow.gameObject.SetActive(false);
            button5_green.gameObject.SetActive(true);

            button6_red.gameObject.SetActive(false);
            button6_yellow.gameObject.SetActive(false);
            button6_green.gameObject.SetActive(true);

            button7_red.gameObject.SetActive(false);
            button7_yellow.gameObject.SetActive(false);
            button7_green.gameObject.SetActive(true);

            button8_red.gameObject.SetActive(false);
            button8_yellow.gameObject.SetActive(false);
            button8_green.gameObject.SetActive(true);

            button9_red.gameObject.SetActive(false);
            button9_yellow.gameObject.SetActive(false);
            button9_green.gameObject.SetActive(true);

            button10_red.gameObject.SetActive(false);
            button10_yellow.gameObject.SetActive(true);
            button10_green.gameObject.SetActive(false);
        }
    }







    /* 
     public void Button1 ()
     {
         SceneManager.LoadScene(2);
     }

     public void Button2()
     {
         Color buttonColor;
         buttonColor = button1.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(3);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }


     public void Button3()
     {
         Color buttonColor;
         buttonColor = button2.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(4);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button4()
     {
         Color buttonColor;
         buttonColor = button3.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(5);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button5()
     {
         Color buttonColor;
         buttonColor = button4.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(6);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button6()
     {
         Color buttonColor;
         buttonColor = button5.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(7);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button7()
     {
         Color buttonColor;
         buttonColor = button6.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(8);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button8()
     {
         Color buttonColor;
         buttonColor = button7.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(9);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button9()
     {
         Color buttonColor;
         buttonColor = button8.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(10);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }
     public void Button10()
     {
         Color buttonColor;
         buttonColor = button9.GetComponent<Image>().color;

         if (buttonColor.Equals(Color.green))
         {
             SceneManager.LoadScene(11);
         }
         else
         {
             panel.gameObject.SetActive(true);
         }
     }


     */


    public void bt1_green()
    {
        SceneManager.LoadScene(2);
    }
    public void bt2_green()
    {
        SceneManager.LoadScene(3);
    }
    public void bt3_green()
    {
        SceneManager.LoadScene(4);
    }
    public void bt4_ygreen()
    {
        SceneManager.LoadScene(5);
    }
    public void bt5_green()
    {
        SceneManager.LoadScene(6);
    }
    public void bt6_green()
    {
        SceneManager.LoadScene(7);
    }
    public void bt7_green()
    {
        SceneManager.LoadScene(8);
    }
    public void bt8_green()
    {
        SceneManager.LoadScene(9);
    }
    public void bt9_green()
    {
        SceneManager.LoadScene(10);
    }
    public void bt10_green()
    {
        SceneManager.LoadScene(11);
    }




    public void bt1_yellow()
    {
        SceneManager.LoadScene(2);
    }
    public void bt2_yellow()
    {
        SceneManager.LoadScene(3);
    }
    public void bt3_yellow()
    { 
        SceneManager.LoadScene(4);
    }
    public void bt4_yellow()
    {
        SceneManager.LoadScene(5);
    }
    public void bt5_yellow()
    {
        SceneManager.LoadScene(6);
    }
    public void bt6_yellow()
    {
        SceneManager.LoadScene(7);
    }
    public void bt7_yellow()
    {
        SceneManager.LoadScene(8);
    }
    public void bt8_yellow()
    {
        SceneManager.LoadScene(9);
    }
    public void bt9_yellow()
    {
        SceneManager.LoadScene(10);
    }
    public void bt10_yellow()
    {
        SceneManager.LoadScene(11);
    }




    public void bt1_red()
    {
        panel.gameObject.SetActive(true);
    }
    
    public void bt2_red()
    {
        panel.gameObject.SetActive(true);
    }
    public void bt3_red()
    {
        panel.gameObject.SetActive(true);
    }

    public void bt4_red()
    {
        panel.gameObject.SetActive(true);
    }
    public void bt5_red()
    {
        panel.gameObject.SetActive(true);
    }

    public void bt6_red()
    {
        panel.gameObject.SetActive(true);
    }
    public void bt7_red()
    {
        panel.gameObject.SetActive(true);
    }

    public void bt8_red()
    {
        panel.gameObject.SetActive(true);
    }
    public void bt9_red()
    {
        panel.gameObject.SetActive(true);
    }

    public void bt10_red()
    {
        panel.gameObject.SetActive(true);
    }
}
