using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_slideEndEvent : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;

    private void OnTriggerEnter(Collider other)
    {
        powersA_game_playerMovement playerScript = other.GetComponent<powersA_game_playerMovement>(); //Get player script if available

        if(playerScript != null)
        {
            //Okay, this is a long string of awfulness that allows the trigger to access the cam mover
            //and set it to return to its normal programming
            parentChunk.chunkSystem.gameManager.camMover.trackChunkSystem = false;
            parentChunk.chunkSystem.gameManager.camMover.targetPos = new Vector3(0, 3, -17); //Set cam to correct position for cutscene
            parentChunk.chunkSystem.gameManager.camMover.targetRot = new Vector3(0, 0, 0);
            parentChunk.chunkSystem.gameManager.camMover.percentAfter1Second = 0.01f;

            Destroy(gameObject);
        }
    }
}
