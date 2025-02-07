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
    // Reference to the audio source component for playing sounds
    public AudioSource audio;

    private Vector2 initialPosition; // The initial position of the object
    private Vector2 mousePosition; // The position of the mouse pointer
    private bool locked;// Boolean flag to check if the object is selected or not

    // Variable to store the change in the Y position (used for movement calculations)
    float deltaY;


    // Limits for 2-block obstacles
    private float limita_sus = 1; // Upper limit for 2-block obstacles on the Y axis
    private float limita_jos = -3; // Lower limit for 2-block obstacles on the Y axis

    // Limits for 3-block obstacles
    private float limita_sus2 = 0.5f; // Upper limit for 3-block obstacles on the Y axis
    private float limita_jos2 = -2.5f;  // Lower limit for 3-block obstacles on the Y axis

    public GameObject Obstacle; // Reference to the obstacle GameObject

    // Store the initial position of the object when the game starts
    void Start()
    {
        initialPosition = transform.position; 
    }


    private void OnMouseDown()
    {

        // If the object is not locked, clicking on it means we want to move it
        if (!locked)
        {
            audio.mute = false; // Unmute the audio
            audio.Play(); // Play audio
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y; // Calculate the Y difference between the mouse position and the object
        }
    }

    private void OnMouseDrag()
    {

        if (!locked)
        {

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // Get the position of the mouse in world coordinates
            float finalPosition_y = mousePosition.y - deltaY;  // Calculate the desired final position on the Y-axis

            if (Obstacle.CompareTag("Obstacle_2blocks_y")) // Condition for 2BLOCKS
            {
                // Ensure the object stays within the grid limits
                if (finalPosition_y < -3.0f) // If it goes off the grid on the lower side
                    finalPosition_y = -3.0f; // Keep it at the lower boundary of the grid
                else if (finalPosition_y > 1.0f) // If it goes off the grid on the upper side
                    finalPosition_y = 1.0f; // Keep it at the upper boundary of the grid


                // Instructions to position the object within the grid cells
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

            if (Obstacle.CompareTag("Obstacle_3blocks_y")) // Condition for 3BLOCKS
            {
                // Ensure the object stays within the grid limits
                if (finalPosition_y < -2.5f) // If it goes off the grid on the lower side        -2.5 -1.5 ; -1.5 -0.5 ; -0.5  0.5
                    finalPosition_y = -2.5f; // Keep it at the lower boundary of the grid
                else if (finalPosition_y > 0.5f) // If it goes off the grid on the upper side
                    finalPosition_y = 0.5f; // Keep it at the upper boundary of the grid


                // Instructions for fixing the object's position within the grid cells
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

            // Combine obstacles with 2 blocks on X axis and player
            GameObject[] Obstacles_2blocks_x_player = Obstacles_2blocks_x.Concat(player).ToArray();

            // All obstacles on Y axis
            GameObject[] Obstacles_y = Obstacles_3blocks_y.Concat(Obstacles_2blocks_y).ToArray();
            // All obstacles on X axis + player
            GameObject[] Obstacles_x = Obstacles_3blocks_x.Concat(Obstacles_2blocks_x_player).ToArray();

            // Combine obstacles for 2 blocks and 3 blocks for both axes
            GameObject[] Obstacles1 = Obstacles_x.Concat(Obstacles_y).ToArray();    // Obstacles for 2 blocks
            // Obstacles for 3 blocks. Cannot have other 3-block obstacles on the same line as it would block the path
            GameObject[] Obstacles2 = Obstacles_x.Concat(Obstacles_2blocks_y).ToArray();

            // Identify only obstacles of type "2-blocks_y"
            if (Obstacle.CompareTag("Obstacle_2blocks_y"))
            {

                // Loop through all obstacles in the "Obstacles1" array (which includes obstacles on both X and Y axes, and the player)
                foreach (GameObject obstacle in Obstacles1)
                {
                    // Get the current position of the obstacle
                    Vector2 pos_obst = obstacle.transform.position;

                    // If the obstacle is on the X-axis (either 2-blocks_x, 3-blocks_x, or the player)
                    if (obstacle.CompareTag("Obstacle_2blocks_x") || obstacle.CompareTag("Obstacle_3blocks_x") || obstacle.CompareTag("player"))
                    {
                        // Only obstacles that are within a certain range of the initial X-position can block the movement
                        if ((pos_obst.x >= initialPosition.x - 1) && (pos_obst.x <= initialPosition.x + 1))
                        {
                            // If the obstacle is above the current position on the Y-axis, update the upper limit to avoid collision
                            if (limita_sus > (pos_obst.y - 1.5f) && initialPosition.y < pos_obst.y)
                                limita_sus = pos_obst.y - 1.5f; // Set the upper limit

                            // If the obstacle is below the current position on the Y-axis, update the lower limit to avoid collision
                            if (limita_jos < (pos_obst.y + 1.5f) && initialPosition.y > pos_obst.y)
                                limita_jos = pos_obst.y + 1.5f; // Set the lower limit

                            // If the obstacle is exactly 0.5 units above the current position, adjust the final Y position
                            if (pos_obst.y - finalPosition_y == 0.5f)
                                finalPosition_y = pos_obst.y - 1.5f; // Move the position to avoid collision

                            // If the obstacle is exactly 0.5 units below the current position, adjust the final Y position
                            if (pos_obst.y - finalPosition_y == -0.5f)
                                finalPosition_y = pos_obst.y + 1.5f; // Move the position to avoid collision
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_2blocks_y")) // Check if the obstacle is of type "2-blocks_y"
                    {
                        // Only obstacles that have an X position within the specified range can block the movement
                        if (pos_obst.x == initialPosition.x && pos_obst.y != initialPosition.y) // Check if the obstacle is aligned on the X-axis and not at the same Y position
                        {
                            // If the obstacle is above the current position on the Y-axis, update the upper limit to prevent collision
                            if (limita_sus > (pos_obst.y - 2f) && initialPosition.y < pos_obst.y)
                                limita_sus = pos_obst.y - 2f;   // Set the upper limit based on the obstacle's position

                            // If the obstacle is below the current position on the Y-axis, update the lower limit to prevent collision
                            if (limita_jos < (pos_obst.y + 2f) && initialPosition.y > pos_obst.y)
                                limita_jos = pos_obst.y + 2f; // Set the lower limit based on the obstacle's position

                            // If the obstacle is exactly 1 unit above the current position, adjust the final Y position
                            if (pos_obst.y - finalPosition_y == 1f)
                                finalPosition_y = pos_obst.y - 2f; // Move the position to avoid collision

                            // If the obstacle is exactly 1 unit below the current position, adjust the final Y position
                            if (pos_obst.y - finalPosition_y == -1f)
                                finalPosition_y = pos_obst.y + 2f; // Move the position to avoid collision
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_3blocks_y")) // Check if the obstacle is of type "3-blocks_y"
                    {
                        // Only obstacles that have an X position matching the initial position can block the movement
                        if (pos_obst.x == initialPosition.x) // Check if the obstacle is aligned on the X-axis
                        {
                            // If the obstacle is above the current position on the Y-axis, update the upper limit to prevent collision
                            if (limita_sus > (pos_obst.y - 2.5f) && initialPosition.y < pos_obst.y)
                                limita_sus = pos_obst.y - 2.5f; // Set the upper limit based on the obstacle's position

                            // If the obstacle is below the current position on the Y-axis, update the lower limit to prevent collision
                            if (limita_jos < (pos_obst.y + 2.5f) && initialPosition.y > pos_obst.y)
                                limita_jos = pos_obst.y + 2.5f; // Set the lower limit based on the obstacle's position

                            // If the obstacle is exactly 1.5 units above the current position, adjust the final Y position
                            if (pos_obst.y - finalPosition_y == 1.5f)
                                finalPosition_y = pos_obst.y - 2.5f; // Move the position to avoid collision

                            // If the obstacle is exactly 1.5 units below the current position, adjust the final Y position
                            if (pos_obst.y - finalPosition_y == -1.5f)
                                finalPosition_y = pos_obst.y + 2.5f; // Move the position to avoid collision
                        }
                    }
                }

                if (finalPosition_y > limita_sus) // Check if the final Y position exceeds the upper limit
                {
                    finalPosition_y = limita_sus; // Set the final Y position to the upper limit
                    limita_sus = 1f; // Reset the upper limit to its default value
                }

                if (finalPosition_y < limita_jos) // Check if the final Y position goes below the lower limit
                {
                    finalPosition_y = limita_jos;  // Set the final Y position to the lower limit
                    limita_jos = -3f; // Reset the lower limit to its default value
                }

                // Update the player position while keeping the initial X position
                transform.position = new Vector2(initialPosition.x, finalPosition_y);
                // Set the initial position to the current position (to update the reference)
                initialPosition = transform.position;
            }

            if (Obstacle.CompareTag("Obstacle_3blocks_y")) // Check if the obstacle is of type "3blocks_y"
            {
                foreach (GameObject obstacle in Obstacles2) // Iterate through all obstacles in Obstacles2
                {
                    Vector2 pos_obst = obstacle.transform.position; // Get the position of the current obstacle

                    if (obstacle.CompareTag("Obstacle_2blocks_x") || obstacle.CompareTag("Obstacle_3blocks_x") || obstacle.CompareTag("player"))
                    {
                        // Check if the obstacle is within 1 unit on the x-axis of the current position
                        if ((pos_obst.x >= initialPosition.x - 1) && (pos_obst.x <= initialPosition.x + 1))
                        {
                            // Adjust the upper limit based on the position of the obstacle
                            if (limita_sus2 > (pos_obst.y - 2f) && initialPosition.y < pos_obst.y)
                                limita_sus2 = pos_obst.y - 2f; // Set the upper limit to the position of the obstacle minus 2 units

                            // Adjust the lower limit based on the position of the obstacle
                            if (limita_jos2 < (pos_obst.y + 2f) && initialPosition.y > pos_obst.y)
                                limita_jos2 = pos_obst.y + 2f; // Set the lower limit to the position of the obstacle plus 2 units

                            // Check for collisions and adjust the final Y position to avoid overlap
                            if (pos_obst.y - finalPosition_y == 1f)
                                finalPosition_y = pos_obst.y - 2f;  // If the obstacle is 1 unit above the final position, adjust the Y position

                            if (pos_obst.y - finalPosition_y == -1f)
                                finalPosition_y = pos_obst.y + 2f; // If the obstacle is 1 unit below the final position, adjust the Y position
                        }
                    }

                    if (obstacle.CompareTag("Obstacle_2blocks_y")) // Check if the obstacle is of type "2blocks_y"
                    {
                        {
                            if (pos_obst.x == initialPosition.x) // If the obstacle is on the same x position as the player
                            {
                                // Adjust the upper limit for 2-block obstacles
                                if (limita_sus2 > (pos_obst.y - 2.5f) && initialPosition.y < pos_obst.y)
                                    limita_sus2 = pos_obst.y - 2.5f; // Set the upper limit for 2-block obstacles

                                // Adjust the lower limit for 2-block obstacles
                                if (limita_jos2 < (pos_obst.y + 2.5f) && initialPosition.y > pos_obst.y)
                                    limita_jos2 = pos_obst.y + 2.5f; // Set the lower limit for 2-block obstacles

                                // Check for collisions with 2-block obstacles and adjust the final Y position accordingly
                                if (pos_obst.y - finalPosition_y == 1.5f)
                                    finalPosition_y = pos_obst.y - 2.5f; // Adjust final Y position if collision occurs

                                if (pos_obst.y - finalPosition_y == -1.5f)
                                    finalPosition_y = pos_obst.y + 2.5f; // Adjust final Y position if collision occurs
                            }
                        }
                    }


                    if (finalPosition_y > limita_sus2) // Check if the final Y position exceeds the upper limit
                    {
                        finalPosition_y = limita_sus2;  // If it does, set the Y position to the upper limit
                        limita_sus2 = 0.5f; // Reset the upper limit to 0.5
                    }

                    if (finalPosition_y < limita_jos2) // Check if the final Y position is below the lower limit
                    {
                        finalPosition_y = limita_jos2; // If it does, set the Y position to the lower limit
                        limita_jos2 = -2.5f; // Reset the lower limit to -2.5
                    }

                    // Update the position of the player on the x-axis while keeping the Y position within the set limits
                    transform.position = new Vector2(initialPosition.x, finalPosition_y);
                    // Set the initial position to the current position after the movement
                    initialPosition = transform.position;
                }
            }
        }
    }

}

