using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_boss1EncounterStartEvent : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;
    [Tooltip("The boss prefab.")]
    public GameObject boss;
    private GameObject bossInstance;

    private bool eventStarted = false; //A bool to indicate if the cutscene event started
    private bool camTargetSet = false; //A bool to indicate if the cam target position for the cutscene has been set
    private bool bossSpawned = false; //A bool to indicate if the boss has spawned
    private bool secondShakeStarted = false; //A bool to indicate that the second shake has started
    private float bossFallWaitCountdown = 0.75f; //A float used for a countdown until the boss falls once cam target has been set
    private float finalWaitCountdown = 0.15f; //A float used for a countdown in the final stretch of the cutscene

    private void Update()
    {
        if(eventStarted)
        {
            if(parentChunk.chunkSystem.gameManager.camMover.shakeIntensity <= 0.05) //Once the shake stops, continue
            {
                if(!camTargetSet && parentChunk.chunkSystem.gameManager.missionMode) parentChunk.chunkSystem.gameManager.camMover.targetPos = new Vector3(-8, 4, -17); //Set cam to correct position for cutscene
                camTargetSet = true;

                if (!bossSpawned) //If boss is not spawned yet, spawn the boss
                {
                    bossInstance = Instantiate(boss, new Vector3(-12.5f, 24, 0), Quaternion.identity); //Spawn the boss
                    parentChunk.chunkSystem.gameManager.currentBoss = bossInstance;
                    bossSpawned = true;
                }
                bossFallWaitCountdown -= Time.deltaTime;

                if(bossFallWaitCountdown < 0 || !parentChunk.chunkSystem.gameManager.missionMode) //Once the countdown for the boss to fall has been completed, continue
                {
                    //Move the boss into place
                    if (bossInstance.transform.position.y >= 4) bossInstance.transform.position -= new Vector3(0, Time.deltaTime * 20, 0);
                    else //Once boss in moved in place...
                    {
                        bossInstance.transform.position = new Vector3(-12.5f, 4, 0); //Set the boss to the correct position
                        if (!secondShakeStarted) parentChunk.chunkSystem.gameManager.camMover.shakeIntensity = 6; //Shake the camera
                        else if (parentChunk.chunkSystem.gameManager.camMover.shakeIntensity <= 0.05) //Once the shake stops again, continue
                        {
                            finalWaitCountdown -= Time.deltaTime; //countdown final segment

                            if (finalWaitCountdown < 0 || !parentChunk.chunkSystem.gameManager.missionMode)
                            {
                                parentChunk.chunkSystem.gameManager.camMover.targetPos = new Vector3(0, 3, -17); //Set cam to correct position for cutscene
                                parentChunk.chunkSystem.gameManager.time = 1; //Set chunk system time to 1 to continue gameplay
                                parentChunk.chunkSystem.gameManager.playerScript.disabled = false; //Enable player movement now that the script is finished

                                if (parentChunk.chunkSystem.gameManager.camMover.transform.position.x >= -0.02f)
                                {
                                    parentChunk.chunkSystem.pauseEnvChange = true; //Pause the env change for the boss
                                    parentChunk.chunkSystem.gameManager.camMover.percentAfter1Second = 0.935f; //Set cam to move slowly
                                    parentChunk.chunkSystem.gameManager.camMover.targetPos = new Vector3(-5, 3, -17); //Set cam to slowly move left during boss
                                    parentChunk.chunkSystem.gameManager.camMover.targetRot = new Vector3(0, -3.25f, -1.5f); //Set cam to slowly rotate left during boss

                                    Destroy(gameObject); //Destroy the gameobject
                                }

                            }
                        }

                        secondShakeStarted = true; // The second shake has begun.
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
            if(parentChunk.chunkSystem.gameManager.missionMode)
            {
                parentChunk.chunkSystem.gameManager.time = 0; //Set chunk system time to 0 to stop current gameplay
                parentChunk.chunkSystem.gameManager.playerScript.disabled = true; //Disable player movement for the cutscene
            }

            parentChunk.chunkSystem.gameManager.camMover.shakeIntensity = 2; //Shake the camera

            eventStarted = true; //The event has started
        }
    }
}
