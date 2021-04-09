using UnityEngine;
using UnityEngine.UI;

public class powersA_powerupIndicator : MonoBehaviour
{
    public powersA_game_playerMovement playerScript;
    public CanvasGroup imageAlpha;

    private void Start()
    {
        imageAlpha.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //set image alpha based on if player has powerup
        if (playerScript.secChanceObtained) imageAlpha.alpha = powersA_util_animMath.Slide(imageAlpha.alpha, 1, 0.01f);
        else imageAlpha.alpha = powersA_util_animMath.Slide(imageAlpha.alpha, 0, 0.01f);
    }
}
