using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.EventSystems;

using Vector2 = UnityEngine.Vector2;
using System.Runtime.InteropServices.ComTypes;



public class player_movement: MonoBehaviour
{

   

    private Vector2 initialPosition; // pozitia initiala a player-ului
    private Vector2 mousePosition; // pozitia mouse-ului
    private bool locked; //verifica daca am apasat sau nu pe obiect
    float deltaX;   
    private float limita_stanga=-2f; // limita grid la stanga
    private float limita_dreapta=2f; // limita grid la dreapta

    public AudioSource audio; // feedback audio la apasarea obiectului
    public GameObject nextLvlCanvas; //canvasul pentru next level - se activeaza atunci cand player-ul se afla pe pozitia rosie

 

    void Start()
    {
        initialPosition = transform.position; // pozitia initiala a obiectului 
       
    }

    private void OnMouseDown()
    {
        if (!locked) // daca nu e locked, am dat click pe el ceea ce inseamna ca vrem sa il mutam
        {

            audio.mute = false;
            audio.Play(); //feedback audio
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x; //pozitia mouse-ului pe x

        }

    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //pozitia mouse-ului
            //CONDITIE PENTRU 2BLOCKS
            float finalPosition_x = mousePosition.x - deltaX; // positia la care se va muta player-ul pe x
            if (finalPosition_x < -2.0f) // daca iese din grid in partea stanga
                finalPosition_x = -2.0f; // ramane la capatul din stanga a gridului
            else if (finalPosition_x > 2f) // daca iese din grid in partea dreapta
                finalPosition_x = 2.0f; // ramane la capatul din dreapta a gridului




            //instructiuni pentru pozitionarea fixa a obiectului in casutele grid-ului. 
            //luam fiecare bloc al gridului si verificam care este marginea mai apropiata de player si il pozitonam acolo
            if(finalPosition_x > -2.0f && finalPosition_x <-1.0f)
            {
                if ( Math.Abs(-2.0f - finalPosition_x) < Math.Abs(-1.0f - finalPosition_x ))
                    finalPosition_x = -2.0f;
                else
                    finalPosition_x = -1.0f;
            }
            else if (finalPosition_x > -1.0f && finalPosition_x < 0f)
            {
                if (Math.Abs(-1f - finalPosition_x) < Math.Abs(-finalPosition_x))
                    finalPosition_x = -1f;
                else
                    finalPosition_x = 0f;
            }
            else if (finalPosition_x > 0f && finalPosition_x < 1f)
            {
                if (Math.Abs(-finalPosition_x) < Math.Abs(1 - finalPosition_x))
                    finalPosition_x = 0f;
                else
                    finalPosition_x = 1f;
            }
            else if (finalPosition_x > 1 && finalPosition_x <2)
            {
                if (Math.Abs(1 - finalPosition_x) < Math.Abs(2 - finalPosition_x))
                    finalPosition_x = 1f;
                else
                    finalPosition_x = 2f;
            }


           // pentru player, singurele obstacole care ne pot sta in cale sunt cele de pe y
            //utilizam tag-urile pentru a crea doi vectori cu toate obstacolele de pe y
            GameObject[] Obstacles_3blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_y");
            GameObject[] Obstacles_2blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_y");

            //concatenam cele doua array-uri intr-un singur array de obstacole
            GameObject[] Obstacles = Obstacles_3blocks_y.Concat(Obstacles_2blocks_y).ToArray();

           
          
            //parcurgem  vectorul de obstacole si gasim obstacolul care este cel mai apropiat de pozitia player-ului la stanga si la dreapta si
            // stabilit 2 limite intre care se poate misca, astfel incat nu poate sa treaca prin obstacole sau sa le "sara"
            foreach (GameObject obstacle in Obstacles)
            {
                //pozitia obstacolului
                Vector2 pos_obst = obstacle.transform.position;

                //daca pivotul obstacolelor se afla pe y la aceste puncte, inseamna ca sunt pozitionate pe linia de deplasare a player-ului
                if (pos_obst.y == 0.5f || pos_obst.y == -0.5f || pos_obst.y == -1.5f || pos_obst.y == 0f || pos_obst.y == -1)
                {
                    //daca limita la dreapta este mai mare de pozitia maxima la care se poate muta player-ul pentru obstacolul curent, iar pozitia initiala a player-ului
                    // este mai mica pe x decat pozitia obstacolului, inseamna ca obstacolul se afla in dreapta si setam limita de dreapta
                    if (limita_dreapta > (pos_obst.x - 1.5f) && initialPosition.x < pos_obst.x)
                        limita_dreapta = pos_obst.x - 1.5f;

                    //daca limita la stanga este mai mica decat pozitia la care player-ul se va ciocni de obstacol si pozitia player-ului se afla la dreapta obstacolului,
                    // inseamna ca trebuie sa setam limita de stanga
                    if (limita_stanga < (pos_obst.x + 1.5f) && initialPosition.x > pos_obst.x)
                        limita_stanga = pos_obst.x + 1.5f;

                    //actualizam pozitia finala in functie de obstacolul verificat curent
                    // asta daca incercem sa ajungem la el, si putem testa daca am ajuns la el prin diferenta coordonatelor care ne da 0.5 (obstacolul de afla la dreapta)
                    if(pos_obst.x - finalPosition_x == 0.5f)
                        finalPosition_x = pos_obst.x - 1.5f;

                    //ctualizam pozitia finala in functie de obstacolul verificat curent
                    // asta daca incercem sa ajungem la el, si putem testa daca am ajuns la el prin diferenta coordonatelor care ne da -0.5 (obstacolul de afla la stanga)
                    if (pos_obst.x - finalPosition_x == -0.5f)
                        finalPosition_x = pos_obst.x + 1.5f;
                }
                

            }

            
            

            
            //daca pozitia la care vrem sa mergem este mai mare decat limita din dreapta, nu putem ajunge acolo pentru ca am sari peste obstacol
            if (finalPosition_x > limita_dreapta)
            {
                //setam pozitia finala la limita din dreapta
                finalPosition_x = limita_dreapta;
                // resetam limita, la punctul cel mai din stanga la care pivotul player-ului poate ajunge
                limita_dreapta = 2f;
            }

            // daca pozitia la care vrem sa mergem este mai mica decat limita din stanga, nu putem ajunge acolo pentru ca am sari peste obstacol
            if(finalPosition_x < limita_stanga)
            {
                //setal pozitia finala la limita din dreapta
                finalPosition_x = limita_stanga;
                // resetam limita, la punctul cel mai din stanga la care pivotul player-ului poate ajunge
                limita_stanga = -2f;
            }

            

            //actualizam pozitia player-ului
            if (finalPosition_x == 2f)
            {
               // Debug.Log("WIN!!!!!!!!");
                

                nextLvlCanvas.SetActive(true);
               

            }
            transform.position = new Vector2(finalPosition_x, initialPosition.y); // player-ul poate sa mearga doar pe x, deci y-ul ramane cel initial
            //pozitia initiala devine pozitia curenta
            initialPosition = transform.position;

   
        }
    }
    
    
 
}


