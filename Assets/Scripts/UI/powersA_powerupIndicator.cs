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
        if (playerScript.secChanceObtained) imageAlpha.alpha = Mathf.Lerp(imageAlpha.alpha, 1, 3 * Time.deltaTime);
        else imageAlpha.alpha = Mathf.Lerp(imageAlpha.alpha, 0, 3 * Time.deltaTime);
    }
}
