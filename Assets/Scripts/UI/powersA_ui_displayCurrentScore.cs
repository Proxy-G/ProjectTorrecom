using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_displayCurrentScore : MonoBehaviour
{
    public powersA_game_gameManager gameManager;
    public Text scoreText;
    public string startingFormat;

    public float scoreUpdateCountdown = 0.0166f; //Countdown used for updating score

    // Start is called before the first frame update
    void Update()
    {
        scoreUpdateCountdown -= Time.deltaTime;

        if(scoreUpdateCountdown < 0) //If countdown complete
        {
            scoreText.text = startingFormat + (int)gameManager.score; //Set text to current score
            scoreUpdateCountdown = 0.0166f; //Reset countdown
        }
    }
}
