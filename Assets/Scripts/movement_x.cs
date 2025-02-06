using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class movement_x : MonoBehaviour
{

    public AudioSource audio;
    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private bool locked;
    float deltaX;
    public GameObject Obstacle;
    //limite pentru 2blocks
    private float limita_stanga = -2f;
    private float limita_dreapta = 2f;

    //limita pentru 3blocks
    private float limita_stanga2 = -1.5f;
    private float limita_dreapta2 = 1.5f;

    void Start()
    {
        initialPosition = transform.position; // pozitia initiala a obiectului 
    }

    private void OnMouseDown()
    {
        if (!locked) // daca nu e locked, am dat click pe el ceea ce inseamna ca vrem sa il mutam
        {
            audio.mute = false;
            audio.Play();
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        }

    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // optinem pozitia mouse-ului
            float finalPosition_x = mousePosition.x - deltaX; // calcula pozitia finala pe x

            if (Obstacle.CompareTag("Obstacle_2blocks_x")) //CONDITIE PENTRU 2BLOCKS
            {

                //CONDITIA SA NU IASA DIN GRID
                if (finalPosition_x < -2.0f) // daca iese din grid in partea stanga 
                    finalPosition_x = -2.0f; // ramane la capatul din stanga a gridului
                else if (finalPosition_x > 2.0f) // daca iese din grid in partea dreapta
                    finalPosition_x = 2.0f; // ramane la capatul din dreapta a gridului


                //instructiuni pentru pozitionarea fixa a obiectului in casutele grid-ului.
                // pentru fiecare casuta in parte se verifica daca pozitionare este facuta mai aproape de marginea din dreapta sau stanga, iar obiectul se pozitioneaza corespunzator
                if (finalPosition_x > -2.0f && finalPosition_x < -1.0f)
                {
                    if (Math.Abs(-2.0f - finalPosition_x) < Math.Abs(-1.0f - finalPosition_x))
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
                else if (finalPosition_x > 1 && finalPosition_x < 2)
                {
                    if (Math.Abs(1 - finalPosition_x) < Math.Abs(2 - finalPosition_x))
                        finalPosition_x = 1f;
                    else
                        finalPosition_x = 2f;
                }
                // incheiere instructiuni pentru pozitionarea fixa a obiectului in casutele grid-ului
            }


            else if (Obstacle.CompareTag("Obstacle_3blocks_x")) //  CONDITIE PENTRU 3BLOCKS
            {
                //CONDITIA SA NU IASA DIN GRID
                if (finalPosition_x < -1.5f) // daca iese din grid in partea stanga
                    finalPosition_x = -1.5f; // ramane la capatul din stanga a gridului
                else if (finalPosition_x > 1.5f) // daca iese din grid in partea dreapta
                    finalPosition_x = 1.5f; // ramane la capatul din dreapta a gridului

                //instructiuni pentru pozitionarea fixa a obiectului in casutele grid-ului
                if (finalPosition_x > -1.5f && finalPosition_x < -0.5f)
                {
                    if (Math.Abs(-1.5f - finalPosition_x) < Math.Abs(-0.5 - finalPosition_x))
                        finalPosition_x = -1.5f;
                    else
                        finalPosition_x = -0.5f;

                }
                else if (finalPosition_x > -0.5f && finalPosition_x < 0.5f)
                {
                    if (Math.Abs(-0.5f - finalPosition_x) < Math.Abs(0.5f - finalPosition_x))
                        finalPosition_x = -0.5f;
                    else
                        finalPosition_x = 0.5f;
                }
                else if (finalPosition_x > 0.5f && finalPosition_x < 1.5f)
                {
                    if (Math.Abs(0.5f - finalPosition_x) < (1.5f - finalPosition_x))
                        finalPosition_x = 0.5f;
                    else
                        finalPosition_x = 1.5f;
                }
                // incheiere instructiuni pentur pozitionare fixa a obiectului in casutele gridului

            }

            //verificare daca un alt obiect se afla pe pozitia respectiva
            // pentru obst cu 2 blocuri verificam toate celelalte obstacole, dar pentru cele 3 blocuri eliminam obstacolele cu 3 blocuri deoarece s-ar bloca pe linia respectiva

            GameObject[] Obstacles_3blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_y");
            GameObject[] Obstacles_2blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_y");
            GameObject[] Obstacles_3blocks_x = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_x");
            GameObject[] Obstacles_2blocks_x = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_x");


            GameObject[] Obstacles_y = Obstacles_3blocks_y.Concat(Obstacles_2blocks_y).ToArray(); //toate obstacolele pe y
            GameObject[] Obstacles_x = Obstacles_3blocks_x.Concat(Obstacles_2blocks_x).ToArray(); // toate obstacolele pe x

            //
            GameObject[] Obstacles1 = Obstacles_x.Concat(Obstacles_y).ToArray(); // obstacolele pentru 2Blocks
            GameObject[] Obstacles2 = Obstacles_y.Concat(Obstacles_2blocks_x).ToArray(); // obstacolele pentru 3Blocks. Nu pot exista alte obstacole 3blocks pe aceeasi linie pentru ca s-ar bloca linia respectiva


            //pentru obstacolele pe x trebuie sa verificam separat




            if (Obstacle.CompareTag("Obstacle_2blocks_x"))
            {
                //comparam sa vedem daca obstacolul pe y se afla in intervalul [initial_position.y -1 ; initial_position.y +1].
                // daca da si acestea se afla la 0.5f de positioa finala pe x, atunci obstacolul pe carevrem sa il mutam poate sa  mearga doar la 1.5f de acesta, daca obnst e pe y


                //setam limite la stanga si dreapta
                // it's ok! you got it!


                foreach (GameObject obstacle in Obstacles1)
                {

                    Vector2 pos_obst = obstacle.transform.position;

                    if (obstacle.CompareTag("Obstacle_2blocks_y") || obstacle.CompareTag("Obstacle_3blocks_y")) // au acelasi pivot pe x
                    {
                        if ((pos_obst.y >= initialPosition.y - 1) && (pos_obst.y <= initialPosition.y + 1)) // daca blocul se afla pe linia unde este si obstacolul pe care vrem sa-l deplasam
                        {
                            if (limita_dreapta > (pos_obst.x - 1.5f) && initialPosition.x < pos_obst.x)
                                limita_dreapta = pos_obst.x - 1.5f;

                            if (limita_stanga < (pos_obst.x + 1.5f) && initialPosition.x > pos_obst.x)
                                limita_stanga = pos_obst.x + 1.5f;

                            if (pos_obst.x - finalPosition_x == 0.5f)
                                finalPosition_x = pos_obst.x - 1.5f;

                            if (pos_obst.x - finalPosition_x == -0.5f)
                                finalPosition_x = pos_obst.x + 1.5f;
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_2blocks_x"))
                    {
                        if (pos_obst.y == initialPosition.y && pos_obst.x != initialPosition.x) // daca se afla pe linia obstacolului pe care vrem sa il mutam si nu e obstacolul curent
                        {
                            if (limita_dreapta > (pos_obst.x - 2f) && initialPosition.x < pos_obst.x)
                                limita_dreapta = pos_obst.x - 2f;

                            if (limita_stanga < (pos_obst.x + 2f) && initialPosition.x > pos_obst.x)
                                limita_stanga = pos_obst.x + 2f;

                            if (pos_obst.x - finalPosition_x == 1f)
                                finalPosition_x = pos_obst.x - 2f;

                            if (pos_obst.x - finalPosition_x == -1f)
                                finalPosition_x = pos_obst.x + 2f;
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_3blocks_x"))
                    {
                        if (pos_obst.y == initialPosition.y) // daca se afla pe linia obstacolului pe care vrem sa il mutam
                        {
                            if (limita_dreapta > (pos_obst.x - 2.5f) && initialPosition.x < pos_obst.x)
                                limita_dreapta = pos_obst.x - 2.5f;

                            if (limita_stanga < (pos_obst.x + 2.5f) && initialPosition.x > pos_obst.x)
                                limita_stanga = pos_obst.x + 2.5f;

                            if (pos_obst.x - finalPosition_x == 1.5f)
                                finalPosition_x = pos_obst.x - 2.5f;

                            if (pos_obst.x - finalPosition_x == -1.5f)
                                finalPosition_x = pos_obst.x + 2.5f;
                        }
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
                if (finalPosition_x < limita_stanga)
                {
                    //setal pozitia finala la limita din dreapta
                    finalPosition_x = limita_stanga;
                    // resetam limita, la punctul cel mai din stanga la care pivotul player-ului poate ajunge
                    limita_stanga = -2f;
                }

                //actualizam pozitia player-ului
                transform.position = new Vector2(finalPosition_x, initialPosition.y); // player-ul poate sa mearga doar pe x, deci y-ul ramane cel initial
                                                                                      //pozitia initiala devine pozitia curenta
                initialPosition = transform.position;
            }




            if (Obstacle.CompareTag("Obstacle_3blocks_x"))
            {


                foreach (GameObject obstacle in Obstacles2)
                {

                    Vector2 pos_obst = obstacle.transform.position;

                    if (obstacle.CompareTag("Obstacle_2blocks_y") || obstacle.CompareTag("Obstacle_3blocks_y")) // au acelasi pivot pe x
                    {
                        if ((pos_obst.y >= initialPosition.y - 1) && (pos_obst.y <= initialPosition.y + 1)) // daca blocul se afla pe linia unde este si obstacolul pe care vrem sa-l deplasam
                        {
                            if (limita_dreapta2 > (pos_obst.x - 2f) && initialPosition.x < pos_obst.x)
                                limita_dreapta2 = pos_obst.x - 2f;

                            if (limita_stanga2 < (pos_obst.x + 2f) && initialPosition.x > pos_obst.x)
                                limita_stanga2 = pos_obst.x + 2f;

                            if (pos_obst.x - finalPosition_x == 1f)
                                finalPosition_x = pos_obst.x - 2f;

                            if (pos_obst.x - finalPosition_x == -1f)
                                finalPosition_x = pos_obst.x + 2f;
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_2blocks_x"))
                    {
                        if (pos_obst.y == initialPosition.y ) // daca se afla pe linia obstacolului pe care vrem sa il mutam
                        {
                            if (limita_dreapta2 > (pos_obst.x - 2.5f) && initialPosition.x < pos_obst.x)
                                limita_dreapta2 = pos_obst.x - 2.5f;

                            if (limita_stanga2 < (pos_obst.x + 2.5f) && initialPosition.x > pos_obst.x)
                                limita_stanga2 = pos_obst.x + 2.5f;

                            if (pos_obst.x - finalPosition_x == 1.5f)
                                finalPosition_x = pos_obst.x - 2.5f;

                            if (pos_obst.x - finalPosition_x == -1.5f)
                                finalPosition_x = pos_obst.x + 2.5f;
                        }
                    }

                }


                //daca pozitia la care vrem sa mergem este mai mare decat limita din dreapta, nu putem ajunge acolo pentru ca am sari peste obstacol
                if (finalPosition_x > limita_dreapta2)
                {
                    //setam pozitia finala la limita din dreapta
                    finalPosition_x = limita_dreapta2;
                    // resetam limita, la punctul cel mai din stanga la care pivotul player-ului poate ajunge
                    limita_dreapta2 = 1.5f;
                }

                // daca pozitia la care vrem sa mergem este mai mica decat limita din stanga, nu putem ajunge acolo pentru ca am sari peste obstacol
                if (finalPosition_x < limita_stanga2)
                {
                    //setal pozitia finala la limita din dreapta
                    finalPosition_x = limita_stanga2;
                    // resetam limita, la punctul cel mai din stanga la care pivotul player-ului poate ajunge
                    limita_stanga2 = -1.5f;
                }

                //actualizam pozitia player-ului
                transform.position = new Vector2(finalPosition_x, initialPosition.y); // player-ul poate sa mearga doar pe x, deci y-ul ramane cel initial
                                                                                      //pozitia initiala devine pozitia curenta
                initialPosition = transform.position;



            }
        }


    }
}

