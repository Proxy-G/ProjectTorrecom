using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_game_gap : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;
    [Tooltip("At this point, the player has hit the point of no return and the chunk system should stop.")]
    public float failHeight = -0.01f;

    private powersA_game_playerMovement refPlayer;

    private void OnTriggerStay(Collider other)
    {
        refPlayer = other.GetComponent<powersA_game_playerMovement>(); //Detect if the other collider is the player

        if (refPlayer && refPlayer.transform.position.y < failHeight) ObstacleHit(); //If player script is not null, perform actions
    }

    public void ObstacleHit()
    {
        if (parentChunk) parentChunk.chunkSystem.gameManager.gapFallEase = true; //Stop the chunk system
        refPlayer.inGap = true; //Player has fallen in gap-ignore all collisions
        this.enabled = false; //Disable the obstacle to prevent more OnTriggerEnter code from running
    }
}
