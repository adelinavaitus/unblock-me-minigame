using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class movement_y : MonoBehaviour
{

    public AudioSource audio;
    private Vector2 initialPosition; // pozitia initiala a obiectului
    private Vector2 mousePosition; // pozitia mouse-ului
    private bool locked; // verifica daca este selectat sau nu obiectul
    float  deltaY;
    //limite pentru 2blocks
    private float limita_sus = 1; 
    private float limita_jos = -3; 

    //limite pentru 3blocks
    private float limita_sus2 = 0.5f;
    private float limita_jos2 = -2.5f;

    public GameObject Obstacle;
    void Start()
    {
        initialPosition = transform.position; // pozitia initiala a obiectului 
    }

    private void OnMouseDown()
    {
        if (!locked) // daca nu e locked, am dat click pe el ceea ce inseamna ca vrem sa il mutam
        {
            audio.mute = false;
            audio.Play(); // feedback audio
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y; // pozitia mouse-ului
        }

    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
          
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // pozitia mouse-ului
            float finalPosition_y = mousePosition.y - deltaY; // positia la care se va muta player-ul pe y

           
            if (Obstacle.CompareTag("Obstacle_2blocks_y")) //CONDITIE PENTRU 2BLOCKS
            {
                //CONDITIA SA NU IASA DIN GRID
                if (finalPosition_y < -3.0f) // daca iese din grid in partea stanga
                    finalPosition_y = -3.0f; // ramane la capatul din stanga a gridului    
                else if (finalPosition_y > 1.0f) // daca iese din grid in partea dreapta
                    finalPosition_y = 1.0f; // ramane la capatul din dreapta a gridului


                //instructiuni pentru pozitionarea fixa a obiectului in casutele grid-ului.
                if (finalPosition_y > -3f && finalPosition_y < -2f)
                {
                    if (Math.Abs(-3f - finalPosition_y) < Math.Abs(-2f - finalPosition_y))
                        finalPosition_y = -3f;
                    else
                        finalPosition_y = -2f;
                }
                else if (finalPosition_y > -2f && finalPosition_y < -1f)
                {
                    if (Math.Abs(-2f - finalPosition_y) < Math.Abs(-1f - finalPosition_y))
                        finalPosition_y = -2f;
                    else
                        finalPosition_y = -1f;
                }
                else if (finalPosition_y > -1f && finalPosition_y < 0f)
                {
                    if (Math.Abs(-1f - finalPosition_y) < Math.Abs(-finalPosition_y))
                        finalPosition_y = -1f;
                    else
                        finalPosition_y = 0f;
                }
                else if (finalPosition_y > 0f && finalPosition_y < 1f)
                {
                    if (Math.Abs(-finalPosition_y) < Math.Abs(1 - finalPosition_y))
                        finalPosition_y = 0f;
                    else
                        finalPosition_y = 1f;
                }
            }

            if (Obstacle.CompareTag("Obstacle_3blocks_y")) //CONDITIE PENTRU 3BLOCKS
            {
                //CONDITIA SA NU IASA DIN GRID
                if (finalPosition_y < -2.5f) // daca iese din grid in partea stanga         -2.5 -1.5 ; -1.5 -0.5 ; -0.5  0.5
                    finalPosition_y = -2.5f; // ramane la capatul din stanga a gridului
                else if (finalPosition_y > 0.5f) // daca iese din grid in partea dreapta
                    finalPosition_y = 0.5f; // ramane la capatul din dreapta a gridului


                //instructiuni pentru pozitionarea fixa a obiectului in casutele grid-ului.
                if (finalPosition_y > -2.5f && finalPosition_y < -1.5f)
                {
                    if (Math.Abs(-2.5f - finalPosition_y) < Math.Abs(-1.5f - finalPosition_y))
                        finalPosition_y = -2.5f;
                    else
                        finalPosition_y = -1.5f;
                }
                else if (finalPosition_y > -1.5f && finalPosition_y < -0.5f)
                {
                    if (Math.Abs(-1.5 - finalPosition_y) < Math.Abs(-0.5 - finalPosition_y))
                        finalPosition_y = -1.5f;
                    else
                        finalPosition_y = -0.5f;
                  
                }
                else if (finalPosition_y > -0.5f && finalPosition_y < 0.5f)
                {
                    if (Math.Abs(-0.5f - finalPosition_y) < Math.Abs(0.5f - finalPosition_y))
                        finalPosition_y = -0.5f;
                    else
                        finalPosition_y = 0.5f;
                }
            }



            GameObject[] Obstacles_3blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_y");
            GameObject[] Obstacles_2blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_y");
            GameObject[] Obstacles_3blocks_x = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_x");
            GameObject[] Obstacles_2blocks_x = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_x");
            GameObject[] player = GameObject.FindGameObjectsWithTag("player");

            GameObject[] Obstacles_2blocks_x_player = Obstacles_2blocks_x.Concat(player).ToArray(); // obstacolele de 2 blocuri si player-ul


            GameObject[] Obstacles_y = Obstacles_3blocks_y.Concat(Obstacles_2blocks_y).ToArray(); //toate obstacolele pe y
            GameObject[] Obstacles_x = Obstacles_3blocks_x.Concat(Obstacles_2blocks_x_player).ToArray(); // toate obstacolele pe x + player
            

            //
            GameObject[] Obstacles1 = Obstacles_x.Concat(Obstacles_y).ToArray(); // obstacolele pentru 2Blocks
            GameObject[] Obstacles2 = Obstacles_x.Concat(Obstacles_2blocks_y).ToArray(); // obstacolele pentru 3Blocks. Nu pot exista alte obstacole 3blocks pe aceeasi linie pentru ca s-ar bloca linia respectiva


            // identificam doar obstacolele de tip 2 blocuri pe y
            if (Obstacle.CompareTag("Obstacle_2blocks_y"))
            {

                // parcurgem vectorul de obstacole 
                foreach (GameObject obstacle in Obstacles1)
                {
                    //pozitia obstacolului curent
                    Vector2 pos_obst = obstacle.transform.position;

                    //daca obstacolele sunt pe x 
                    if (obstacle.CompareTag("Obstacle_2blocks_x") || obstacle.CompareTag("Obstacle_3blocks_x") || obstacle.CompareTag("player"))
                    {
                        // doar obstacolele care au pozitia pe x cuprinsa in intervalul de la mai jos, pot sta in calea obstacolului pe care dorim sa l mutam.
                        if ((pos_obst.x >= initialPosition.x - 1) && (pos_obst.x <= initialPosition.x + 1))
                        {
                            if (limita_sus > (pos_obst.y - 1.5f) && initialPosition.y < pos_obst.y)
                                limita_sus = pos_obst.y - 1.5f;

                            if (limita_jos < (pos_obst.y + 1.5f) && initialPosition.y > pos_obst.y)
                                limita_jos = pos_obst.y + 1.5f;

                            if (pos_obst.y - finalPosition_y == 0.5f)
                                finalPosition_y = pos_obst.y - 1.5f;

                            if (pos_obst.y - finalPosition_y == -0.5f)
                                finalPosition_y = pos_obst.y + 1.5f;
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_2blocks_y") )
                    {
                        // doar obstacolele care au pozitia pe x cuprinsa in intervalul de la mai jos, pot sta in calea obstacolului pe care dorim sa l mutam.
                        if (pos_obst.x == initialPosition.x && pos_obst.y != initialPosition.y)
                        {
                            if (limita_sus > (pos_obst.y - 2f) && initialPosition.y < pos_obst.y)
                                limita_sus = pos_obst.y - 2f;

                            if (limita_jos < (pos_obst.y + 2f) && initialPosition.y > pos_obst.y)
                                limita_jos = pos_obst.y + 2f;

                            if (pos_obst.y - finalPosition_y == 1f)
                                finalPosition_y = pos_obst.y - 2f;

                            if (pos_obst.y - finalPosition_y == -1f)
                                finalPosition_y = pos_obst.y + 2f;
                        } 
                    }

                    if (obstacle.CompareTag("Obstacle_3blocks_y"))
                    {
                        // doar obstacolele care au pozitia pe x cuprinsa in intervalul de la mai jos, pot sta in calea obstacolului pe care dorim sa l mutam.
                        if (pos_obst.x == initialPosition.x )
                        {
                            if (limita_sus > (pos_obst.y - 2.5f) && initialPosition.y < pos_obst.y)
                                limita_sus = pos_obst.y - 2.5f;

                            if (limita_jos < (pos_obst.y + 2.5f) && initialPosition.y > pos_obst.y)
                                limita_jos = pos_obst.y + 2.5f;

                            if (pos_obst.y - finalPosition_y == 1.5f)
                                finalPosition_y = pos_obst.y - 2.5f;

                            if (pos_obst.y - finalPosition_y == -1.5f)
                                finalPosition_y = pos_obst.y + 2.5f;
                        }
                    }
                }

                if (finalPosition_y > limita_sus)
                {
                    finalPosition_y = limita_sus;
                    limita_sus = 1f;
                }

                if (finalPosition_y < limita_jos)
                {
                    finalPosition_y = limita_jos;
                    limita_jos = -3f;
                }

                transform.position = new Vector2(initialPosition.x, finalPosition_y); // player-ul poate sa mearga doar pe x, deci y-ul ramane cel initial
                initialPosition = transform.position;//pozitia initiala devine pozitia curenta
            }



            if (Obstacle.CompareTag("Obstacle_3blocks_y"))
            {
                foreach (GameObject obstacle in Obstacles2)
                {
                    Vector2 pos_obst = obstacle.transform.position;

                    if (obstacle.CompareTag("Obstacle_2blocks_x") || obstacle.CompareTag("Obstacle_3blocks_x") || obstacle.CompareTag("player"))
                    {

                        if ((pos_obst.x >= initialPosition.x - 1) && (pos_obst.x <= initialPosition.x + 1)) // daca blocul se afla pe linia unde este si obstacolul pe care vrem sa-l deplasam
                        {
                            if (limita_sus2 > (pos_obst.y - 2f) && initialPosition.y  < pos_obst.y)
                                limita_sus2 = pos_obst.y - 2f;

                            if (limita_jos2 < (pos_obst.y + 2f) && initialPosition.y > pos_obst.y)
                                limita_jos2 = pos_obst.y + 2f;

                            if (pos_obst.y - finalPosition_y == 1f)
                                finalPosition_y = pos_obst.y - 2f;

                            if (pos_obst.y - finalPosition_y == -1f)
                                finalPosition_y = pos_obst.y + 2f;
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_2blocks_y"))
                    {
                        if (pos_obst.x == initialPosition.x)
                        {
                            if (limita_sus2 > (pos_obst.y - 2.5f) && initialPosition.y < pos_obst.y)
                                limita_sus2 = pos_obst.y - 2.5f;

                            if (limita_jos2 < (pos_obst.y + 2.5f) && initialPosition.y > pos_obst.y)
                                limita_jos2 = pos_obst.y + 2.5f;

                            if (pos_obst.y - finalPosition_y == 1.5f)
                                finalPosition_y = pos_obst.y - 2.5f;

                            if (pos_obst.y - finalPosition_y == -1.5f)
                                finalPosition_y = pos_obst.y + 2.5f;
                        }
                    }


                }


                if (finalPosition_y > limita_sus2)
                {
                    finalPosition_y = limita_sus2;
                    limita_sus2 = 0.5f;
                }

                if (finalPosition_y < limita_jos2)
                {
                    finalPosition_y = limita_jos2;
                    limita_jos2 = -2.5f;
                }

                transform.position = new Vector2(initialPosition.x, finalPosition_y); // player-ul poate sa mearga doar pe x, deci y-ul ramane cel initial
                initialPosition = transform.position;//pozitia initiala devine pozitia curenta

            }

        }
    }


}

