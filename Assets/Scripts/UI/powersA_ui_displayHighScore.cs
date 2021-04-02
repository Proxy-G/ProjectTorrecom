using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_displayHighScore : MonoBehaviour
{
    public powersA_game_gameManager gameManager;
    public Text scoreText;
    public string startingFormat;

    // Start is called before the first frame update
    void Update()
    {
        if (gameManager.highScoreCheck)
        {
            scoreText.text = startingFormat + PlayerPrefs.GetInt("High Score"); //set text to high score
            enabled = false; //disable the script
        }
    }
}
