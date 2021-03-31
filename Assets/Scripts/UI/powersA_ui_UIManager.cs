using UnityEngine;

public class powersA_ui_UIManager : MonoBehaviour
{
    public powersA_game_playerMovement playerScript;
    public bool inGame = false;

    [Space(10)]

    public CanvasGroup mainMenuUI;
    public CanvasGroup gameplayUI;
    public CanvasGroup pauseUI;
    public CanvasGroup deadUI;

    private void Start()
    {
        //Activate the gameplay UI
        mainMenuUI.alpha = 1;
        mainMenuUI.interactable = true;

        //Activate the gameplay UI
        gameplayUI.alpha = 0;
        gameplayUI.interactable = false;

        //Deactivate the pause UI
        pauseUI.alpha = 0;
        pauseUI.interactable = false;

        //Deactivate the gameplay UI
        deadUI.alpha = 0;
        deadUI.interactable = false;

        //Enable the UI groups to true to ensure they are active
        mainMenuUI.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(true);
        deadUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inGame)
        {
            mainMenuUI.alpha = 1; //Transition gameplay UI to invisible
            mainMenuUI.interactable = true; //Allow no interaction on the gameplay UI
            mainMenuUI.blocksRaycasts = true; //Stop raycasts from going further

            gameplayUI.alpha = 0; //Transition gameplay UI to invisible
            gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
            gameplayUI.blocksRaycasts = false; //Allow raycasts to go through

            pauseUI.alpha = 0; //Transition pause UI to invisible
            pauseUI.interactable = false; //Allow no interaction on pause UI
            pauseUI.blocksRaycasts = false; //Allow raycasts to go through

            deadUI.alpha = 0; //Transition death UI to invisible
            deadUI.interactable = false; //Allow interaction on the death UI
            deadUI.blocksRaycasts = false; //Allow raycasts to go through
        }
        else
        {
            mainMenuUI.interactable = false; //Allow no interaction on the gameplay UI
            mainMenuUI.blocksRaycasts = false; //Allow raycasts to go through

            if (playerScript.isDead) // If player is dead
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
            }
            else if (Time.timeScale == 0) //If player is paused
            {
                gameplayUI.alpha = Mathf.Lerp(mainMenuUI.alpha, 0, Time.unscaledDeltaTime * 5); //Transition gameplay UI to invisible
                gameplayUI.interactable = false; //Allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = false; //Allow raycasts to go through gameplay UI

                pauseUI.alpha = Mathf.Lerp(pauseUI.alpha, 1, Time.unscaledDeltaTime * 5); //Transition pause UI to visible
                pauseUI.interactable = true; //Allow interaction on pause UI
                pauseUI.blocksRaycasts = true; //Stop raycasts from going further

                deadUI.alpha = 0; //Transition the death UI alpha to 1
                deadUI.interactable = false; //Allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //Allow raycasts to go through death UI
            }
            else //If player is in gameplay
            {
                gameplayUI.alpha = Mathf.Lerp(mainMenuUI.alpha, 1, Time.unscaledDeltaTime); //Transition gameplay UI to visible
                gameplayUI.interactable = true; //Allow interaction with gameplay UI
                gameplayUI.blocksRaycasts = true; //Stop raycasts from going further

                pauseUI.alpha = 0; //Make pause UI invisible
                pauseUI.interactable = false; //Allow no interaction on the pause UI
                pauseUI.blocksRaycasts = false; //Allow raycasts to go through pause UI

                deadUI.alpha = 0; //Make death UI invisible
                deadUI.interactable = false; //Allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //Allow raycasts to go through death UI
            }
        }
    }

    public void ChangeInGame(bool value)
    {
        inGame = value;
    }
}
