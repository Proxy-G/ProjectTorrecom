using UnityEngine;

public class powersA_game_gameManager : MonoBehaviour
{
    public powersA_game_playerMovement player;
    public powersA_game_chunkSystem mainChunkSystem;
    public float score = 0;
    public float time = 1;
    [HideInInspector]
    public bool highScoreCheck = false;
    [HideInInspector]
    public bool gapFallEase = false;

    public void Update()
    {
        if (gapFallEase) time = Mathf.Lerp(time, 0, 0.05f);

        if (!player.isDead) score += Time.deltaTime * 100; //increase score over time if player is alive
        else if (!highScoreCheck)
        {
            //if current score is higher than the high score, then save the current score as the new high score.
            if (PlayerPrefs.GetInt("High Score") < score) PlayerPrefs.SetInt("High Score", (int)score);
            highScoreCheck = true;
        }
    }

    public void ResetLevel()
    {
        mainChunkSystem.ResetChunkSystem(); //reset the chunk system

        player.charController.enabled = false; //disable the character controller cause it will reset the transform position
        player.transform.position = new Vector3(-3, 0.085f, 0); //reset player position
        player.charController.enabled = true; //reenable the character controller

        player.isDead = false; //reset player's death state
        player.inGap = false; //reset player to not be in gap

        score = 0; //reset the score

        gapFallEase = false; //make sure gap fall ease is turned off
        time = 1; //reset time
    }
}
