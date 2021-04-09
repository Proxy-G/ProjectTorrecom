using UnityEngine;

public class powersA_ui_UIManager : MonoBehaviour
{
    public powersA_game_playerMovement playerScript;
    public bool inGame = false;
    public bool lockMenu = false;
    public bool missionComplete = false;
    public int currentMainMenu = 0;

    [Space(10)]

    public CanvasGroup mainMenuUI;
    public CanvasGroup baseMainUI;
    public CanvasGroup modeMainUI;
    public CanvasGroup gameplayUI;
    public CanvasGroup pauseUI;
    public CanvasGroup deadUI;
    public CanvasGroup missionCompleteUI;

    private void Start()
    {
        //Activate the gameplay UI
        mainMenuUI.alpha = 1;
        mainMenuUI.interactable = true;

        //Set base menu UI to be used
        baseMainUI.alpha = 1; 
        baseMainUI.interactable = true;
        baseMainUI.blocksRaycasts = true;

        //Set mode menu UI invisible
        modeMainUI.alpha = 0; 
        modeMainUI.interactable = false;
        modeMainUI.blocksRaycasts = false;

        //Activate the gameplay UI
        gameplayUI.alpha = 0;
        gameplayUI.interactable = false;

        //Deactivate the pause UI
        pauseUI.alpha = 0;
        pauseUI.interactable = false;

        //Deactivate the gameplay UI
        deadUI.alpha = 0;
        deadUI.interactable = false;

        //Deactivate the mission complete UI
        missionCompleteUI.alpha = 0;
        missionCompleteUI.interactable = false;

        //Enable the UI groups to true to ensure they are active
        mainMenuUI.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(true);
        deadUI.gameObject.SetActive(true);
        missionCompleteUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inGame)
        {
            mainMenuUI.alpha = 1; //Transition gameplay UI to invisible
            mainMenuUI.interactable = true; //Allow no interaction on the gameplay UI
            mainMenuUI.blocksRaycasts = true; //Stop raycasts from going further

            if (currentMainMenu == 0) //If on the base menu
            {
                baseMainUI.alpha = Mathf.Lerp(baseMainUI.alpha, 1, Time.unscaledDeltaTime * 10); //Set base menu UI to be used
                baseMainUI.interactable = true;
                baseMainUI.blocksRaycasts = true;

                modeMainUI.alpha = Mathf.Lerp(modeMainUI.alpha, 0, Time.unscaledDeltaTime * 10); //Set mode menu UI invisible
                modeMainUI.interactable = false;
                modeMainUI.blocksRaycasts = false;
            }
            else if (currentMainMenu == 1)
            {
                baseMainUI.alpha = Mathf.Lerp(baseMainUI.alpha, 0, Time.unscaledDeltaTime * 10); //Set base menu UI invisible
                baseMainUI.interactable = false;
                baseMainUI.blocksRaycasts = false;

                modeMainUI.alpha = Mathf.Lerp(modeMainUI.alpha, 1, Time.unscaledDeltaTime * 10); //Set mode menu UI to be used
                modeMainUI.interactable = true;
                modeMainUI.blocksRaycasts = true;
            }

            gameplayUI.alpha = 0; //Transition gameplay UI to invisible
            gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
            gameplayUI.blocksRaycasts = false; //Allow raycasts to go through

            pauseUI.alpha = 0; //Transition pause UI to invisible
            pauseUI.interactable = false; //Allow no interaction on pause UI
            pauseUI.blocksRaycasts = false; //Allow raycasts to go through

            deadUI.alpha = 0; //Transition death UI to invisible
            deadUI.interactable = false; //Allow interaction on the death UI
            deadUI.blocksRaycasts = false; //Allow raycasts to go through

            missionCompleteUI.alpha = 0; //Transition mission complete UI to visible
            missionCompleteUI.interactable = false; //Allow no interaction on the mission complete UI
            missionCompleteUI.blocksRaycasts = false; //Allow raycasts to go through mission complete UI
        }
        else
        {
            mainMenuUI.interactable = false; //Allow no interaction on the gameplay UI
            mainMenuUI.blocksRaycasts = false; //Allow raycasts to go through

            if(missionComplete)
            {
                missionCompleteUI.alpha = 1; //Transition mission complete UI to visible
                missionCompleteUI.interactable = true; //Allow interaction on the mission complete UI
                missionCompleteUI.blocksRaycasts = true; //Stop raycasts from going further

                gameplayUI.alpha = 0; //Set pause UI to invisible
                gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = false; //Allow raycasts to go through gameplay UI

                pauseUI.alpha = 0; //Set pause UI to invisible
                pauseUI.interactable = false; //Allow no interaction on pause UI
                pauseUI.blocksRaycasts = false; //Allow raycasts to go through

                deadUI.alpha = 0; //Set pause UI to invisible
                deadUI.interactable = false; //Allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //Allow raycasts to go through death UI
            }
            else if (playerScript.isDead) // If player is dead
            {
                gameplayUI.alpha = powersA_util_animMath.Slide(gameplayUI.alpha, 0, 0.01f); //Transition gameplay UI to invisible
                gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = false; //Allow raycasts to go through

                pauseUI.alpha = 0; //Set pause UI to invisible
                pauseUI.interactable = false; //Allow no interaction on pause UI
                pauseUI.blocksRaycasts = false; //Allow raycasts to go through

                deadUI.alpha = powersA_util_animMath.Slide(deadUI.alpha, 1, 0.01f); //Transition death UI to visible
                deadUI.interactable = true; //Allow interaction on the death UI
                deadUI.blocksRaycasts = true; //Stop raycasts from going further

                missionCompleteUI.alpha = 0; //Transition mission complete UI to visible
                missionCompleteUI.interactable = false; //Allow no interaction on the mission complete UI
                missionCompleteUI.blocksRaycasts = false; //Allow raycasts to go through mission complete UI
            }
            else if (Time.timeScale == 0) //If player is paused
            {
                gameplayUI.alpha = Mathf.Lerp(gameplayUI.alpha, 0, Time.unscaledDeltaTime * 10); //Transition gameplay UI to invisible
                gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = false; //Allow raycasts to go through gameplay UI

                pauseUI.alpha = Mathf.Lerp(pauseUI.alpha, 1, Time.unscaledDeltaTime * 10); //Transition pause UI to visible
                pauseUI.interactable = true; //Allow interaction on pause UI
                pauseUI.blocksRaycasts = true; //Stop raycasts from going further

                deadUI.alpha = 0; //Transition the death UI alpha to 1
                deadUI.interactable = false; //Allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //Allow raycasts to go through death UI

                missionCompleteUI.alpha = 0; //Transition mission complete UI to visible
                missionCompleteUI.interactable = false; //Allow no interaction on the mission complete UI
                missionCompleteUI.blocksRaycasts = false; //Allow raycasts to go through mission complete UI
            }
            else if (lockMenu)
            {
                gameplayUI.alpha = Mathf.Lerp(gameplayUI.alpha, 0, Time.unscaledDeltaTime * 10); //Transition gameplay UI to invisible
                gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = true; //Stop raycasts from going further

                pauseUI.alpha = Mathf.Lerp(pauseUI.alpha, 0, Time.unscaledDeltaTime * 10); //Make pause UI invisible
                pauseUI.interactable = false; //Allow no interaction on the pause UI
                pauseUI.blocksRaycasts = false; //Allow raycasts to go through pause UI

                deadUI.alpha = 0; //Make death UI invisible
                deadUI.interactable = false; //Allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //Allow raycasts to go through death UI

                missionCompleteUI.alpha = 0; //Transition mission complete UI to visible
                missionCompleteUI.interactable = false; //Allow no interaction on the mission complete UI
                missionCompleteUI.blocksRaycasts = false; //Allow raycasts to go through mission complete UI
            }
            else //If player is in gameplay
            {
                gameplayUI.alpha = Mathf.Lerp(gameplayUI.alpha, 1, Time.unscaledDeltaTime * 10); //Transition gameplay UI to visible
                gameplayUI.interactable = true; //Allow interaction with gameplay UI
                gameplayUI.blocksRaycasts = true; //Stop raycasts from going further

                pauseUI.alpha = Mathf.Lerp(pauseUI.alpha, 0, Time.unscaledDeltaTime * 10); //Make pause UI invisible
                pauseUI.interactable = false; //Allow no interaction on the pause UI
                pauseUI.blocksRaycasts = false; //Allow raycasts to go through pause UI

                deadUI.alpha = 0; //Make death UI invisible
                deadUI.interactable = false; //Allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //Allow raycasts to go through death UI

                missionCompleteUI.alpha = 0; //Transition mission complete UI to visible
                missionCompleteUI.interactable = false; //Allow no interaction on the mission complete UI
                missionCompleteUI.blocksRaycasts = false; //Allow raycasts to go through mission complete UI
            }
        }
    }

    public void ChangeInGame(bool value)
    {
        inGame = value;
    }

    public void ChangeMissionComplete(bool value)
    {
        missionComplete = value;
    }

    public void ChangeCurrentMainMenu (int value)
    {
        currentMainMenu = value;
    }
}
