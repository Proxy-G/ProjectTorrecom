using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_mission1Complete : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;

    private bool eventStarted = false; //A bool to indicate if the cutscene event started

    private void Update()
    {
        //If transition fade has completed
        if (eventStarted && parentChunk.chunkSystem.gameManager.transitionFade.targetAlpha == parentChunk.chunkSystem.gameManager.transitionFade.canvasGroup.alpha)
        {
            parentChunk.chunkSystem.ClearChunkSystem(); //Clear main chunk system
            parentChunk.chunkSystem.enabled = false; //Disable the main chunk system

            parentChunk.chunkSystem.gameManager.uiManager.lockMenu = false; //Allow menus to be used again
            parentChunk.chunkSystem.gameManager.uiManager.missionComplete = true; //Bring up mission complete UI
            parentChunk.chunkSystem.gameManager.transitionFade.targetAlpha = 0; //Fade in from black

            Debug.Log(Time.timeScale);

            Destroy(gameObject);
        }
    }

    // Start initial events once the player enters
    void OnTriggerEnter(Collider other)
    {
        powersA_game_playerMovement playerScript = other.GetComponent<powersA_game_playerMovement>(); //Get player script if available

        if (playerScript != null)
        {
            parentChunk.chunkSystem.gameManager.transitionFade.ChangeTargetAlpha(1); //Set mission to fade out

            //Deactivate the character controller
            parentChunk.chunkSystem.gameManager.time = 0;
            parentChunk.chunkSystem.gameManager.charController.enabled = false;
            parentChunk.chunkSystem.gameManager.playerScript.enabled = false;

            eventStarted = true; //The event has started
        }
    }
}
