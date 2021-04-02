using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_displayCurrentScore : MonoBehaviour
{
    public powersA_game_gameManager gameManager;
    public Text scoreText;
    public string startingFormat;

    // Start is called before the first frame update
    void Update()
    {
        scoreText.text = startingFormat + (int)gameManager.score; //set text to current score
    }
}
