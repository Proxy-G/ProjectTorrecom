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
        //activate the gameplay UI
        mainMenuUI.alpha = 1;
        mainMenuUI.interactable = true;

        //activate the gameplay UI
        gameplayUI.alpha = 0;
        gameplayUI.interactable = false;

        //deactivate the pause UI
        pauseUI.alpha = 0;
        pauseUI.interactable = false;

        //deactivate the gameplay UI
        deadUI.alpha = 0;
        deadUI.interactable = false;

        //enable the UI groups to true to ensure they are active
        gameplayUI.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(true);
        deadUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inGame)
        {
            mainMenuUI.alpha = powersA_util_animMath.Slide(mainMenuUI.alpha, 1, 0.01f); //transition gameplay UI to invisible
            mainMenuUI.interactable = true; //allow no interaction on the gameplay UI
            mainMenuUI.blocksRaycasts = true; //stop raycasts from going further

            gameplayUI.alpha = powersA_util_animMath.Slide(gameplayUI.alpha, 0, 0.01f); //transition gameplay UI to invisible
            gameplayUI.interactable = false; //allow no interaction on the gameplay UI
            gameplayUI.blocksRaycasts = false; //allow raycasts to go through

            pauseUI.alpha = powersA_util_animMath.Slide(pauseUI.alpha, 0, 0.01f); //transition gameplay UI to invisible
            pauseUI.interactable = false; //allow no interaction on pause UI
            pauseUI.blocksRaycasts = false; //allow raycasts to go through

            deadUI.alpha = powersA_util_animMath.Slide(deadUI.alpha, 0, 0.01f); //transition gameplay UI to invisible
            deadUI.interactable = false; //allow interaction on the death UI
            deadUI.blocksRaycasts = false; //stop raycasts from going further
        }
        else
        {
            mainMenuUI.interactable = false; //allow no interaction on the gameplay UI
            mainMenuUI.blocksRaycasts = false; //stop raycasts from going further

            if (playerScript.isDead) // if player is dead
            {
                gameplayUI.alpha = powersA_util_animMath.Slide(gameplayUI.alpha, 0, 0.01f); //transition gameplay UI to invisible
                gameplayUI.interactable = false; //allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = false; //allow raycasts to go through

                pauseUI.alpha = 0; //set pause UI to invisible
                pauseUI.interactable = false; //allow no interaction on pause UI
                pauseUI.blocksRaycasts = false; //allow raycasts to go through

                deadUI.alpha = powersA_util_animMath.Slide(deadUI.alpha, 1, 0.01f); //transition death UI to visible
                deadUI.interactable = true; //allow interaction on the death UI
                deadUI.blocksRaycasts = true; //stop raycasts from going further
            }
            else if (Time.timeScale == 0) //if player is not dead, but paused
            {
                gameplayUI.alpha = Mathf.Lerp(mainMenuUI.alpha, 0, Time.unscaledDeltaTime * 5); //transition gameplay UI to invisible
                gameplayUI.interactable = false; //allow no interaction on the gameplay UI
                gameplayUI.blocksRaycasts = false; //allow raycasts to go through gameplay UI

                pauseUI.alpha = Mathf.Lerp(pauseUI.alpha, 1, Time.unscaledDeltaTime * 5); //transition pause UI to visible
                pauseUI.interactable = true; //allow interaction on pause UI
                pauseUI.blocksRaycasts = true; //stop raycasts from going further

                deadUI.alpha = 0; //transition the death UI alpha to 1
                deadUI.interactable = false; //allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //allow raycasts to go through death UI
            }
            else //if player is not dead, but not paused
            {
                gameplayUI.alpha = 1; //show gameplay UI
                gameplayUI.interactable = true; //allow interaction with gameplay UI
                gameplayUI.blocksRaycasts = true; //stop raycasts from going further

                pauseUI.alpha = 0; //make pause UI invisible
                pauseUI.interactable = false; //allow no interaction on the pause UI
                pauseUI.blocksRaycasts = false; //allow raycasts to go through pause UI

                deadUI.alpha = 0; //make death UI invisible
                deadUI.interactable = false; //allow no interaction on the death UI
                deadUI.blocksRaycasts = false; //allow raycasts to go through death UI
            }
        }
    }

    public void ChangeInGame(bool value)
    {
        inGame = value;
    }
}
