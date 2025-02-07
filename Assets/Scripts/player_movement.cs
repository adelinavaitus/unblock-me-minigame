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
    private Vector2 initialPosition; // The initial position of the player
    private Vector2 mousePosition; // The current position of the mouse on the screen
    private bool locked; // Checks if the player has clicked or interacted with the object
    float deltaX;   // The change in the X position (could be used for dragging or movement calculations)

    private float limita_stanga=-2f; // The left boundary of the grid (determines how far left the player can move)
    private float limita_dreapta=2f; // The right boundary of the grid (determines how far right the player can move)

    public AudioSource audio; // Audio feedback when interacting with the object
    public GameObject nextLvlCanvas;  // The canvas for the next level - this will be activated when the player reaches the red position

    void Start()
    {
        initialPosition = transform.position; // Store the initial position of the object when the game starts
    }

    private void OnMouseDown()
    {
        if (!locked) // If the object is not locked, it means the player clicked on it and wants to move it
        {
            audio.mute = false; // Ensure the audio is unmuted when interacting with the object
            audio.Play(); // Play audio feedback when the object is clicked

            // Calculate the difference between the mouse's X position and the object's X position
            // This is used to maintain the correct offset when the object is dragged
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)  // If the object is not locked, the player can drag it
        {
            // Get the current mouse position in world coordinates
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the final position based on the mouse's x position and the initial deltaX offset
            float finalPosition_x = mousePosition.x - deltaX;

            // Check if the object would go beyond the left boundary of the grid
            if (finalPosition_x < -2.0f) // If the object moves beyond the left limit
                finalPosition_x = -2.0f; // Keep the object at the leftmost edge of the grid
            else if (finalPosition_x > 2f) // Check if the object would go beyond the right boundary of the grid
                finalPosition_x = 2.0f; // Keep the object at the rightmost edge of the grid

            // Instructions for positioning the object fixed in the grid cells.
            // We check each block of the grid and determine which edge is closer to the player's position, then position the object there.
            if (finalPosition_x > -2.0f && finalPosition_x <-1.0f)
            {
                // Check which side (-2.0f or -1.0f) is closer to the object's position
                if ( Math.Abs(-2.0f - finalPosition_x) < Math.Abs(-1.0f - finalPosition_x ))
                    finalPosition_x = -2.0f; // Position at the leftmost grid boundary
                else
                    finalPosition_x = -1.0f; // Position at the next grid point
            }
            else if (finalPosition_x > -1.0f && finalPosition_x < 0f)
            {
                // Check if the object is closer to -1 or 0 on the X-axis
                if (Math.Abs(-1f - finalPosition_x) < Math.Abs(-finalPosition_x))
                    finalPosition_x = -1f; // Position closer to -1
                else
                    finalPosition_x = 0f; // Position closer to 0
            }
            else if (finalPosition_x > 0f && finalPosition_x < 1f)
            {
                // Check if the object is closer to 0 or 1 on the X-axis
                if (Math.Abs(-finalPosition_x) < Math.Abs(1 - finalPosition_x))
                    finalPosition_x = 0f;  // Position closer to 0
                else 
                    finalPosition_x = 1f; // Position closer to 1
            }
            else if (finalPosition_x > 1 && finalPosition_x <2)
            {
                // Check if the object is closer to 1 or 2 on the X-axis
                if (Math.Abs(1 - finalPosition_x) < Math.Abs(2 - finalPosition_x))
                    finalPosition_x = 1f; // Position closer to 1
                else
                    finalPosition_x = 2f; // Position at the rightmost grid boundary
            }

            // For the player, the only obstacles that can block the path are those on the Y-axis
            // We use tags to create two separate arrays containing all obstacles on the Y-axis

            // Find all objects with the tag "Obstacle_3blocks_y" (obstacles that occupy 3 blocks on the Y-axis)
            GameObject[] Obstacles_3blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_3blocks_y");
            // Find all objects with the tag "Obstacle_2blocks_y" (obstacles that occupy 2 blocks on the Y-axis)
            GameObject[] Obstacles_2blocks_y = GameObject.FindGameObjectsWithTag("Obstacle_2blocks_y");

            // Concatenate the two arrays into a single array of obstacles (combining both 3-block and 2-block obstacles)
            GameObject[] Obstacles = Obstacles_3blocks_y.Concat(Obstacles_2blocks_y).ToArray();


            // Loop through the obstacles and find the one closest to the player's position on the left and right.
            // Set movement limits to prevent the player from passing through obstacles or "jumping" over them.
            foreach (GameObject obstacle in Obstacles)
            {
                // Get the position of the current obstacle
                Vector2 pos_obst = obstacle.transform.position;

                // If the obstacle is positioned along the movement line of the player (on the Y-axis)
                if (pos_obst.y == 0.5f || pos_obst.y == -0.5f || pos_obst.y == -1.5f || pos_obst.y == 0f || pos_obst.y == -1)
                {
                    // If the right limit is greater than the maximum position the player can move to for the current obstacle
                    // and the player's initial position is to the left of the obstacle, the obstacle is to the right
                    // Set the right limit to be the obstacle's position minus a buffer (1.5f)
                    if (limita_dreapta > (pos_obst.x - 1.5f) && initialPosition.x < pos_obst.x)
                        limita_dreapta = pos_obst.x - 1.5f;

                    // If the left limit is smaller than the position where the player will collide with the obstacle,
                    // and the player's initial position is to the right of the obstacle, set the left limit
                    if (limita_stanga < (pos_obst.x + 1.5f) && initialPosition.x > pos_obst.x)
                        limita_stanga = pos_obst.x + 1.5f;

                    // Update the final position based on the current obstacle
                    // This checks if the player is trying to reach the obstacle on the right
                    // If the difference between the player's final position and the obstacle's position is 0.5, 
                    // it means the player has reached the obstacle on the right, so adjust the final position to the left of the obstacle.
                    if (pos_obst.x - finalPosition_x == 0.5f)
                        finalPosition_x = pos_obst.x - 1.5f;

                    // Update the final position based on the current obstacle
                    // This checks if the player is trying to reach the obstacle on the left
                    // If the difference between the player's final position and the obstacle's position is -0.5, 
                    // it means the player has reached the obstacle on the left, so adjust the final position to the right of the obstacle.
                    if (pos_obst.x - finalPosition_x == -0.5f)
                        finalPosition_x = pos_obst.x + 1.5f;
                }
            }

            // If the target position is beyond the right limit, we can't reach it because the player would jump over the obstacle
            if (finalPosition_x > limita_dreapta)
            {
                // Set the final position to the right limit (the maximum allowed position)
                finalPosition_x = limita_dreapta;

                // Reset the right limit back to the farthest right position (in this case, 2f)
                limita_dreapta = 2f;
            }

            // If the target position is less than the left limit, we can't reach it because the player would jump over the obstacle
            if (finalPosition_x < limita_stanga)
            {
                // Set the final position to the left limit (the minimum allowed position)
                finalPosition_x = limita_stanga;

                // Reset the left limit back to the farthest left position (in this case, -2f)
                limita_stanga = -2f;
            }

            // Update the player's position
            if (finalPosition_x == 2f)
            {
                // Activate the "Next Level" canvas (indicating the player has won or reached the end of the level)
                nextLvlCanvas.SetActive(true);
            }

            // Update the player's position based on the calculated final position
            // The player's Y position remains the same (initialPosition.y), as movement is restricted to the X axis
            transform.position = new Vector2(finalPosition_x, initialPosition.y);

            // Set the initial position to the player's current position for the next movement cycle
            initialPosition = transform.position;
        }
    }
}


