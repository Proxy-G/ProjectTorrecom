using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_displayHighScore : MonoBehaviour
{
    public powersA_game_gameManager gameManager;
    public Text scoreText;
    public string startingFormat;

    private float highScoreCheckWait = 0.1f; //Countdown used to update high score display every 1/10th of a second

    // Start is called before the first frame update
    void Update()
    {
        highScoreCheckWait -= Time.deltaTime; //Countdown used to make sure new high score is set if needed
        
        if (gameManager.highScoreCheck && highScoreCheckWait < 0) //Set to high score every 1/10th of a second
        {
            scoreText.text = startingFormat + PlayerPrefs.GetInt("High Score", 0);

            highScoreCheckWait = 0.1f;
        }
    }
}
