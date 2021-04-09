using UnityEngine;

public class powersA_game_gameManager : MonoBehaviour
{
    public Camera cam;
    [HideInInspector]
    public powersA_evt_camMover camMover;

    public powersA_game_playerMovement playerScript;
    [HideInInspector]
    public CharacterController charController;
    [Tooltip("This refers to the chunk system used for gameplay.")]
    public powersA_game_chunkSystem mainChunkSystem;
    [Space(10)]
    [Tooltip("This refers to the UI manager system used for all menus.")]
    public powersA_ui_UIManager uiManager;
    public powersA_evt_UIMover mainMenuUI;
    public powersA_ui_transitionFade transitionFade;
    [Space(10)]
    [Tooltip("Current score for the player.")]
    public float score = 0;
    [Tooltip("Current time for the chunk systems.")]
    public float time = 1;
    [HideInInspector]
    public bool highScoreCheck = false;
    [HideInInspector]
    public bool gapFallEase = false;
    [HideInInspector]
    public GameObject currentBoss;
    [HideInInspector]
    public bool bossFinished = false;

    [HideInInspector]
    public bool inGame = false;
    [HideInInspector]
    public bool missionMode = true; //This variable is used if the current gameplay is in mission mode or endless mode
    private bool resetingLevel = false;
    private bool exitingLevel = false;
    private powersA_util_sceneFunctions sceneFuncs;

    public void Start()
    {
        sceneFuncs = GetComponent<powersA_util_sceneFunctions>();
        camMover = cam.GetComponent<powersA_evt_camMover>();
        charController = playerScript.gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (!resetingLevel && inGame)
        {
            if (gapFallEase) time = Mathf.Lerp(time, 0, 0.05f);

            if (!playerScript.isDead && !playerScript.disabled && !missionMode) score += Time.deltaTime * 100; //Increase score over time if player is alive
            else if (playerScript.isDead && !highScoreCheck) //If the player HAS died...
            {
                camMover.playerDead = true; //Tell the cam mover the player is dead

                if(!missionMode) //If in endless mode...
                {
                    //If current score is higher than the high score, then save the current score as the new high score.
                    if (PlayerPrefs.GetInt("High Score") < score) PlayerPrefs.SetInt("High Score", (int)score);
                    highScoreCheck = true;
                }
            }
        }
        else if (resetingLevel) ResetLevel();
        else if (exitingLevel) EnterMainMenu();

        if (camMover.trackChunkSystem) //Check if the cam mover is tracking chunk system
        {
            camMover.chunkSystemSpeed = mainChunkSystem.chunkMoveSpeed; //Set cam mover chunk system speed based on the main chunk system
            camMover.gameManagerTime = time; //Set cam mover reference for game manager time to correct value
        }

        if(currentBoss) //If there is a boss, the end of the boss will be dictated through here
        {
            if (camMover.targetPos == new Vector3(-5, 3, -17) && camMover.transform.position.x < -4.9f && !bossFinished) //If end conditions have been met...
            {
                if (missionMode) //If in mission mode, add ending chunk and disable spawning/despawning chunks
                {
                    mainChunkSystem.overrideChunk = mainChunkSystem.mission1CompleteChunk;
                    mainChunkSystem.spawnChunks = false;
                }
                else if (!missionMode) //Otherwise, set to move to next env
                {
                    mainChunkSystem.pauseEnvChange = false;
                    mainChunkSystem.envChangeCooldown = 0;
                }

                bossFinished = true; //The end of the boss had been setup
            }
        }
    }

    public void ResetLevel()
    {
        resetingLevel = true; //Level is reseting. Do not perform any other actions.

        if (transitionFade.targetAlpha == transitionFade.canvasGroup.alpha)
        {
            mainChunkSystem.enabled = true; //Activate chunk system
            mainChunkSystem.ResetChunkSystem(); //Reset the chunk system
            mainChunkSystem.spawnChunks = true; //Reset the spawn chunks variable
            mainChunkSystem.pauseEnvChange = false; //Reset chunk system env change
            mainChunkSystem.envChangeTime = 40; //Reset env change time

            charController.enabled = false; //Disable the character controller cause it will reset the transform position
            playerScript.transform.position = new Vector3(-3, 0.085f, 0); //Reset player position
            charController.enabled = true; //Reenable the character controller

            camMover.targetPos = new Vector3(0, 3, -17); //Set the camera position
            camMover.transform.position = new Vector3(0, 3, -17); //Set the camera position
            camMover.targetRot = Vector3.zero; //Set the camera rotation
            camMover.transform.rotation = Quaternion.identity;
            camMover.trackChunkSystem = false; //Do not track the chunk system
            camMover.percentAfter1Second = 0.01f; //Reset percent after 1 second
            camMover.playerDead = false; //Reset player is dead settings

            playerScript.enabled = true; //Activate player script
            playerScript.isDead = false; //Reset player's death state
            playerScript.disabled = false; //Reset player's disabled state
            playerScript.inGap = false; //Reset player to not be in gap
            playerScript.secChanceObtained = false; //Reset powerup obtained variable
            playerScript.slideTimer = 0; //Reset slide timer

            if (currentBoss) Destroy(currentBoss); //Remove boss if there is one
            bossFinished = false; //Reset boss finished variable

            score = 0; //Reset the score
            highScoreCheck = false; //Allow high score check again

            gapFallEase = false; //Make sure gap fall ease is turned off
            time = 1; //Reset time for the chunk systems
            sceneFuncs.TimeScaleSet(1); //Set time scale in Unity

            uiManager.missionComplete = false; //Reset mission complete
            uiManager.lockMenu = false; //Reset lock menu option
            uiManager.pauseUI.alpha = 0; //Set pause UI alpha
            uiManager.deadUI.alpha = 0; //Set death UI alpha
            transitionFade.targetAlpha = 0;

            resetingLevel = false; //Level has finished reseting
            inGame = true; //Tell game manager gameplay is stopping
        }
    }

    public void EnterGame()
    {
        mainMenuUI.ChangeTargetPosY(540); //Move main menu UI up
        camMover.ChangeTargetPosY(3); //Move cam
        uiManager.ChangeInGame(true); //Tell ui manager that we are heading into gameplay

        ResetLevel(); //Reset level function works for setting up gameplay from this point
    }

    public void EnterMainMenu()
    {
        exitingLevel = true; //Exiting level currently
        inGame = false; //Tell game manager gameplay is stopping

        if (transitionFade.targetAlpha == transitionFade.canvasGroup.alpha)
        {
            //Set main menu in position
            mainMenuUI.ChangeTargetPosY(-540); //Move main menu UI up
            mainMenuUI.rectPosition.anchoredPosition = new Vector2(0, -540);
            
            //Move cam up to view main menu UI
            camMover.ChangeTargetPosY(12);
            cam.transform.position = new Vector3(cam.transform.position.x, 12, cam.transform.position.z);

            //Set the camera rotation
            camMover.targetRot = Vector3.zero;
            cam.transform.rotation = Quaternion.identity;

            //Deactivate the character controller
            charController.enabled = false;
            playerScript.enabled = false;

            //Clear chunk system and disable script
            mainChunkSystem.ClearChunkSystem();
            mainChunkSystem.enabled = false;

            uiManager.missionComplete = false; //Reset mission complete
            uiManager.lockMenu = false; //Reset menu lock option

            //Set up main menu
            uiManager.ChangeInGame(false); //Tell UI manager that we are exiting to main menu
            uiManager.currentMainMenu = 0; 
            uiManager.baseMainUI.alpha = 1;
            uiManager.modeMainUI.alpha = 0;
            uiManager.gameplayUI.alpha = 0;
            uiManager.pauseUI.alpha = 0;
            uiManager.deadUI.alpha = 0;

            //Set transition to fade in and reset time scale
            transitionFade.targetAlpha = 0;
            sceneFuncs.TimeScaleSet(1);

            score = 0; //Reset the score
            exitingLevel = false; //Exiting to main menu has completed
        }
    }

    public void SetMissionMode(bool value)
    {
        missionMode = value;
    }
}
