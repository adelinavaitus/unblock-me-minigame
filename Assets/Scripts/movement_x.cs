using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
// Alias Unity-specific Vector2 and Vector3 to avoid conflicts with System.Numerics
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class movement_x : MonoBehaviour
{
    // Audio source to play sounds during interaction
    public AudioSource audio;

    // Initial and current positions for movement tracking
    private Vector2 initialPosition;
    private Vector2 mousePosition;

    // Flag to lock movement
    private bool locked;

    // Offset between object and mouse position
    float deltaX;

    // Reference to the obstacle being moved
    public GameObject Obstacle;

    // Movement limits for objects with 2-block and 3-block sizes
    private float limita_stanga = -2f;  // Left limit for 2-blocks
    private float limita_dreapta = 2f;  // Right limit for 2-blocks
    private float limita_stanga2 = -1.5f;    // Left limit for 3-blocks
    private float limita_dreapta2 = 1.5f;    // Right limit for 3-blocks

    void Start()
    {
        // Initialize the position of the object when the game starts
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (!locked) // Only allow interaction if the object is not locked
        {
            // Enable audio playback
            audio.mute = false;
            audio.Play();

            // Calculate offset between the mouse and the object's position
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)    // Allow dragging only if the object is not locked
        {
            // Get the mouse position in world coordinates
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Calculate the tentative final x-position
            float finalPosition_x = mousePosition.x - deltaX;

            // Handle 2-block obstacle logic
            if (Obstacle.CompareTag("Obstacle_2blocks_x")) 
            {

                //CONDITION TO STAY WITHIN GRID LIMITS
                if (finalPosition_x < -2.0f)  // If the object moves beyond the left edge of the grid
                    finalPosition_x = -2.0f; // Keep it at the left edge of the grid
                else if (finalPosition_x > 2.0f) // If the object moves beyond the right edge of the grid
                    finalPosition_x = 2.0f; // Keep it at the right edge of the grid


                // Instructions for snapping the object to grid cells.
                // For each grid cell, it checks whether the position is closer to the right or left edge, and the object is positioned accordingly
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
            }
            else if (Obstacle.CompareTag("Obstacle_3blocks_x")) // CONDITION FOR 3BLOCKS
            {
                // CONDITION TO STAY WITHIN GRID LIMITS
                if (finalPosition_x < -1.5f)     // If the object moves beyond the left edge of the grid
                    finalPosition_x = -1.5f;    // Keep it at the left edge of the grid
                else if (finalPosition_x > 1.5f)    // If the object moves beyond the right edge of the grid
                    finalPosition_x = 1.5f;     // Keep it at the right edge of the grid

                // Instructions for snapping the object to grid cells
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
            }

            // Check if another object is at the same position
            // For obstacles with 2 blocks, we check all other obstacles, but for those with 3 blocks, we exclude other 3-block obstacles to avoid blocking the same line
            GameObject[] Obstacles_3blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_y"); // Find all obstacles with 3 blocks on the Y-axis
            GameObject[] Obstacles_2blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_y"); // Find all obstacles with 2 blocks on the Y-axis
            GameObject[] Obstacles_3blocks_x = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_x");  // Find all obstacles with 3 blocks on the X-axis
            GameObject[] Obstacles_2blocks_x = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_x"); // Find all obstacles with 2 blocks on the X-axis

            // Combine all Y-axis obstacles (both 2-block and 3-block) into a single array
            GameObject[] Obstacles_y = Obstacles_3blocks_y.Concat(Obstacles_2blocks_y).ToArray();
            // Combine all X-axis obstacles (both 2-block and 3-block) into a single array
            GameObject[] Obstacles_x = Obstacles_3blocks_x.Concat(Obstacles_2blocks_x).ToArray();

            // Combine obstacles for 2-block configuration (on both axes)
            GameObject[] Obstacles1 = Obstacles_x.Concat(Obstacles_y).ToArray();
            // Combine all X-axis obstacles (both 2-block and 3-block) into a single array
            GameObject[] Obstacles2 = Obstacles_y.Concat(Obstacles_2blocks_x).ToArray(); 


            if (Obstacle.CompareTag("Obstacle_2blocks_x"))
            {
                // Check if the obstacle on the Y-axis is within the range [initial_position.y - 1; initial_position.y + 1].
                // If yes, and the obstacle is at a distance of 0.5f from the final X position, then the obstacle we want to move can only go to 1.5f away from it, if the obstacle is on the Y-axis.

                // Set the left and right boundaries for movement.
                // it's ok! you got it!

                foreach (GameObject obstacle in Obstacles1)
                {

                    Vector2 pos_obst = obstacle.transform.position;

                    // If the obstacle is along the Y-axis (either 2-block or 3-block), it shares the same X pivot.
                    if (obstacle.CompareTag("Obstacle_2blocks_y") || obstacle.CompareTag("Obstacle_3blocks_y")) 
                    {
                        // Check if the obstacle is within the vertical range of the object we want to move.
                        if ((pos_obst.y >= initialPosition.y - 1) && (pos_obst.y <= initialPosition.y + 1))
                        {
                            // Adjust the right boundary if the obstacle is further left.
                            if (limita_dreapta > (pos_obst.x - 1.5f) && initialPosition.x < pos_obst.x)
                                limita_dreapta = pos_obst.x - 1.5f;

                            // Adjust the left boundary if the obstacle is further right.
                            if (limita_stanga < (pos_obst.x + 1.5f) && initialPosition.x > pos_obst.x)
                                limita_stanga = pos_obst.x + 1.5f;

                            // If the obstacle is exactly 0.5f away on the X-axis, move the object accordingly.
                            if (pos_obst.x - finalPosition_x == 0.5f)
                                finalPosition_x = pos_obst.x - 1.5f;

                            // If the obstacle is exactly -0.5f away on the X-axis, move the object accordingly.
                            if (pos_obst.x - finalPosition_x == -0.5f)
                                finalPosition_x = pos_obst.x + 1.5f;
                        }
                    }

                    // If the obstacle is a 2-block obstacle along the X-axis
                    if (obstacle.CompareTag("Obstacle_2blocks_x"))
                    {
                        // Check if the obstacle is on the same Y-line as the one we want to move, and it's not the same object.
                        if (pos_obst.y == initialPosition.y && pos_obst.x != initialPosition.x)
                        {
                            // Adjust the right boundary if the obstacle is further left
                            if (limita_dreapta > (pos_obst.x - 2f) && initialPosition.x < pos_obst.x)
                                limita_dreapta = pos_obst.x - 2f;

                            // Adjust the left boundary if the obstacle is further right.
                            if (limita_stanga < (pos_obst.x + 2f) && initialPosition.x > pos_obst.x)
                                limita_stanga = pos_obst.x + 2f;

                            // If the obstacle is exactly 1f away on the X-axis, move the object accordingly.
                            if (pos_obst.x - finalPosition_x == 1f)
                                finalPosition_x = pos_obst.x - 2f;

                            // If the obstacle is exactly -1f away on the X-axis, move the object accordingly.
                            if (pos_obst.x - finalPosition_x == -1f)
                                finalPosition_x = pos_obst.x + 2f;
                        }
                    }

                    // If the obstacle is a 3-block obstacle along the X-axis
                    if (obstacle.CompareTag("Obstacle_3blocks_x"))
                    {
                        // Check if the obstacle is on the same Y-line as the one we want to move.
                        if (pos_obst.y == initialPosition.y)
                        {
                            // Adjust the right boundary if the obstacle is further left.
                            if (limita_dreapta > (pos_obst.x - 2.5f) && initialPosition.x < pos_obst.x)
                                limita_dreapta = pos_obst.x - 2.5f;

                            // Adjust the left boundary if the obstacle is further right.
                            if (limita_stanga < (pos_obst.x + 2.5f) && initialPosition.x > pos_obst.x)
                                limita_stanga = pos_obst.x + 2.5f;

                            // If the obstacle is exactly 1.5f away on the X-axis, move the object accordingly.
                            if (pos_obst.x - finalPosition_x == 1.5f)
                                finalPosition_x = pos_obst.x - 2.5f;

                            // If the obstacle is exactly -1.5f away on the X-axis, move the object accordingly.
                            if (pos_obst.x - finalPosition_x == -1.5f)
                                finalPosition_x = pos_obst.x + 2.5f;
                        }
                    }
                }

                // If the desired final position is greater than the right limit, we cannot go there because the player would jump over the obstacle.
                if (finalPosition_x > limita_dreapta)
                {
                    // Set the final position to the right limit to prevent going past the obstacle.
                    finalPosition_x = limita_dreapta;
                    // Reset the right limit to the farthest point on the right the player can go (in this case, 2f).
                    limita_dreapta = 2f;
                }

                // If the desired final position is smaller than the left limit, we cannot go there because the player would jump over the obstacle.
                if (finalPosition_x < limita_stanga)
                {
                    // Set the final position to the left limit to prevent going past the obstacle.
                    finalPosition_x = limita_stanga;
                    // Reset the left limit to the farthest point on the left the player can go (in this case, -2f).
                    limita_stanga = -2f;
                    limita_stanga = -2f;
                }

                // Update the player's position to the new calculated X position, while keeping the Y position the same (since movement is only on the X-axis).
                transform.position = new Vector2(finalPosition_x, initialPosition.y);
                // Set the initial position to the current position for future movement calculations.
                initialPosition = transform.position;
            }

            if (Obstacle.CompareTag("Obstacle_3blocks_x"))
            {
                // Loop through all obstacles in the "Obstacles2" array, which includes obstacles of type 2-blocks_y and 3-blocks_y.
                foreach (GameObject obstacle in Obstacles2)
                {
                    // Get the position of the current obstacle.
                    Vector2 pos_obst = obstacle.transform.position;

                    // Check if the obstacle is a "2blocks_y" or "3blocks_y" (same pivot on the x-axis).
                    if (obstacle.CompareTag("Obstacle_2blocks_y") || obstacle.CompareTag("Obstacle_3blocks_y")) 
                    {
                        // Check if the obstacle is within the vertical range of -1 to +1 from the initial Y position.
                        if ((pos_obst.y >= initialPosition.y - 1) && (pos_obst.y <= initialPosition.y + 1))
                        {
                            // Adjust the right limit to ensure the player does not pass the obstacle on the right.
                            if (limita_dreapta2 > (pos_obst.x - 2f) && initialPosition.x < pos_obst.x)
                                limita_dreapta2 = pos_obst.x - 2f;

                            // Adjust the left limit to ensure the player does not pass the obstacle on the left.
                            if (limita_stanga2 < (pos_obst.x + 2f) && initialPosition.x > pos_obst.x)
                                limita_stanga2 = pos_obst.x + 2f;

                            // If the obstacle is 1 unit away from the final position, adjust the final position.
                            if (pos_obst.x - finalPosition_x == 1f)
                                finalPosition_x = pos_obst.x - 2f;

                            // If the obstacle is -1 unit away from the final position, adjust the final position.
                            if (pos_obst.x - finalPosition_x == -1f)
                                finalPosition_x = pos_obst.x + 2f;
                        }
                    }

                    // If the obstacle is a "2blocks_x" (same pivot on the y-axis).
                    if (obstacle.CompareTag("Obstacle_2blocks_x"))
                    {
                        // Check if the obstacle is on the same horizontal line as the one the player is trying to move on.
                        if (pos_obst.y == initialPosition.y ) // daca se afla pe linia obstacolului pe care vrem sa il mutam
                        {
                            // Adjust the right limit to ensure the player does not pass the obstacle on the right.
                            if (limita_dreapta2 > (pos_obst.x - 2.5f) && initialPosition.x < pos_obst.x)
                                limita_dreapta2 = pos_obst.x - 2.5f;

                            // Adjust the left limit to ensure the player does not pass the obstacle on the left.
                            if (limita_stanga2 < (pos_obst.x + 2.5f) && initialPosition.x > pos_obst.x)
                                limita_stanga2 = pos_obst.x + 2.5f;

                            // If the obstacle is 1.5 units away from the final position, adjust the final position.
                            if (pos_obst.x - finalPosition_x == 1.5f)
                                finalPosition_x = pos_obst.x - 2.5f;

                            // If the obstacle is -1.5 units away from the final position, adjust the final position.
                            if (pos_obst.x - finalPosition_x == -1.5f)
                                finalPosition_x = pos_obst.x + 2.5f;
                        }
                    }
                }

                // If the target position is greater than the right limit, we cannot reach it as it would go past the obstacle.
                if (finalPosition_x > limita_dreapta2)
                {
                    // Set the final position to the right limit.
                    finalPosition_x = limita_dreapta2;
                    // Reset the right limit to the furthest point the player can reach to the right.
                    limita_dreapta2 = 1.5f;
                }

                // If the target position is less than the left limit, we cannot reach it as it would go past the obstacle.
                if (finalPosition_x < limita_stanga2)
                {
                    // Set the final position to the left limit.
                    finalPosition_x = limita_stanga2;
                    // Reset the left limit to the furthest point the player can reach to the left.
                    limita_stanga2 = -1.5f;
                }

                // Update the player's position based on the calculated final position.
                // The player can only move on the x-axis, so the y-coordinate remains the same as the initial y-position.
                transform.position = new Vector2(finalPosition_x, initialPosition.y);

                // Update the initial position to the current position after the move.
                initialPosition = transform.position;
            }
        }
    }
}

