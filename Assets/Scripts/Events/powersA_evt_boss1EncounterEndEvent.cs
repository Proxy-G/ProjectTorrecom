using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_boss1EncounterEndEvent : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;

    private bool eventStarted = false; //A bool to indicate if the cutscene event started
    private bool shakeStarted = false; //A bool to indicate that the second shake has started
    private float endCutsceneInitialWait = 0.5f; //A float used for a countdown until the boss falls once cam target has been set
    private float finalWaitCountdown = 0.15f; //A float used for a countdown in the final stretch of the cutscene

    private void Update()
    {
        if(eventStarted) //If event has started...
        {
            if (endCutsceneInitialWait > 0) endCutsceneInitialWait -= Time.deltaTime; //Countdown before cutscene begins
            else //Once countdown is finished...
            {
                if(parentChunk.chunkSystem.gameManager.missionMode && !shakeStarted) //If in mission mode...
                {
                    parentChunk.chunkSystem.gameManager.time = 0; //Set chunk system time to 0 to stop current gameplay
                    parentChunk.chunkSystem.gameManager.playerScript.disabled = true; //Disable player movement for the cutscene
                    parentChunk.chunkSystem.gameManager.camMover.shakeIntensity = 4; //Shake the camera

                    shakeStarted = true;
                }
                else if(!parentChunk.chunkSystem.gameManager.missionMode && !shakeStarted) //Otherwise...
                {
                    parentChunk.chunkSystem.gameManager.camMover.shakeIntensity = 4; //Shake the camera

                    Destroy(gameObject); //Destroy this event trigger. The event is finished.
                }

                if (parentChunk.chunkSystem.gameManager.camMover.shakeIntensity <= 0.01f) //Once shake is finished...
                {
                    finalWaitCountdown -= Time.deltaTime; //Countdown for final wait

                    if(finalWaitCountdown < 0)
                    {
                        parentChunk.chunkSystem.gameManager.camMover.trackChunkSystem = true;
                        parentChunk.chunkSystem.gameManager.time = 1; //Set chunk system time to 1 to move player ahead

                        Destroy(gameObject); //Destroy this event trigger. The event is finished.
                    }
                }
            }
        }
    }

    // Start initial events once the player enters
    void OnTriggerEnter(Collider other)
    {
        powersA_game_playerMovement playerScript = other.GetComponent<powersA_game_playerMovement>(); //Get player script if available

        if (playerScript != null)
        {
            Destroy(parentChunk.chunkSystem.gameManager.currentBoss); //Destroy the current boss
            parentChunk.chunkSystem.gameManager.bossFinished = false; //Reset boss finished variable
            if (parentChunk.chunkSystem.gameManager.missionMode) parentChunk.chunkSystem.gameManager.uiManager.lockMenu = true; //Player cannot use menus during this cutscene (if mission mode is playing)
            
            eventStarted = true; //The event has started
        }
    }
}
