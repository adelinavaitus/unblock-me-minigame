using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class currentLevel : MonoBehaviour
{

    public GameObject panel;
   
    //public TextMeshPro levelText;
    public TextMeshProUGUI levelText;
    int lvl;
    // Start is called before the first frame update
    void Start()
    {
        lvl = SceneManager.GetActiveScene().buildIndex;
        lvl = lvl - 1;
        levelText.text = lvl.ToString();


   

    }

}
