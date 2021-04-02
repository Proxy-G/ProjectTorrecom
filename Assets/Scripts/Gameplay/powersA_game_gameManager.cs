using UnityEngine;

public class powersA_game_gameManager : MonoBehaviour
{
    public Camera cam;
    private powersA_evt_objectMover camMover;

    public powersA_game_playerMovement playerScript;
    private CharacterController charController;
    [Tooltip("This refers to the chunk system used for gameplay.")]
    public powersA_game_chunkSystem mainChunkSystem;
    [Space(10)]
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
    public bool inGame = false;
    private bool resetingLevel = false;
    private bool exitingLevel = false;
    private powersA_util_sceneFunctions sceneFuncs;

    public void Start()
    {
        sceneFuncs = GetComponent<powersA_util_sceneFunctions>();
        camMover = cam.GetComponent<powersA_evt_objectMover>();
        charController = playerScript.gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (!resetingLevel && inGame)
        {
            if (gapFallEase) time = Mathf.Lerp(time, 0, 0.05f);

            if (!playerScript.isDead) score += Time.deltaTime * 100; //increase score over time if player is alive
            else if (!highScoreCheck)
            {
                //if current score is higher than the high score, then save the current score as the new high score.
                if (PlayerPrefs.GetInt("High Score") < score) PlayerPrefs.SetInt("High Score", (int)score);
                highScoreCheck = true;
            }
        }
        else if (resetingLevel) ResetLevel();
        else if (exitingLevel) EnterMainMenu();
    }

    public void ResetLevel()
    {
        resetingLevel = true; //Level is reseting. Do not perform any other actions.

        if(transitionFade.targetAlpha == transitionFade.canvasGroup.alpha)
        {
            mainChunkSystem.ResetChunkSystem(); //Eeset the chunk system

            charController.enabled = false; //Disable the character controller cause it will reset the transform position
            playerScript.transform.position = new Vector3(-3, 0.085f, 0); //Reset player position
            charController.enabled = true; //Reenable the character controller

            playerScript.isDead = false; //Reset player's death state
            playerScript.inGap = false; //Reset player to not be in gap
            playerScript.secChanceObtained = false; //Reset powerup obtained variable

            score = 0; //Reset the score
            highScoreCheck = false; //Allow high score check again

            gapFallEase = false; //Make sure gap fall ease is turned off
            time = 1; //Reset time for the chunk systems
            sceneFuncs.TimeScaleSet(1); //Set time scale in Unity

            transitionFade.targetAlpha = 0;

            resetingLevel = false; //Level has finished reseting
        }
    }

    public void EnterGame()
    {
        mainMenuUI.ChangeTargetPosY(540); //Move main menu UI up
        camMover.ChangeTargetPosY(3); //Move cam
        uiManager.ChangeInGame(true); //Tell ui manager that we are heading into gameplay
        
        //Get chunk system up and running
        mainChunkSystem.enabled = true;
        mainChunkSystem.ResetChunkSystem();
        
        //Finally activate the character controller
        charController.enabled = true;
        playerScript.enabled = true;

        score = 0; //Reset the score
        highScoreCheck = false; //Allow high score check again
        inGame = true; //Tell game manager that we are in gameplay now
    }

    public void EnterMainMenu()
    {
        exitingLevel = true; //Exiting level currently
        inGame = false; //Tell game manager gameplay is stopping

        if (transitionFade.targetAlpha == transitionFade.canvasGroup.alpha)
        {
            uiManager.ChangeInGame(false); //Tell UI manager that we are exiting to main menu

            //Set main menu in position
            mainMenuUI.ChangeTargetPosY(-540); //Move main menu UI up
            mainMenuUI.rectPosition.anchoredPosition = new Vector2(0, -540);
            
            //Move cam up to view main menu UI
            camMover.ChangeTargetPosY(12);
            cam.transform.position = new Vector3(cam.transform.position.x, 12, cam.transform.position.z);
            
            //Deactivate the character controller
            charController.enabled = false;
            playerScript.enabled = false;

            //Clear chunk system and disable script
            mainChunkSystem.ClearChunkSystem();
            mainChunkSystem.enabled = false;

            //Set transition to fade in and reset time scale
            transitionFade.targetAlpha = 0;
            sceneFuncs.TimeScaleSet(1);

            score = 0; //Reset the score
            exitingLevel = false; //Exiting to main menu has completed
        }
    }
}
