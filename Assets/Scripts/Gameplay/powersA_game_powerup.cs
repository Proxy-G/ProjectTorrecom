using UnityEngine;

public class powersA_game_powerup : MonoBehaviour
{
    [Tooltip("Assign the powerup obtain SFX here.")]
    public AudioClip obtainSFX;


    private powersA_game_playerMovement refPlayer;


    private void OnTriggerEnter(Collider other)
    {
        refPlayer = other.GetComponent<powersA_game_playerMovement>(); //Detect if the other collider is the player

        if (refPlayer) {
            refPlayer.secChanceObtained = true; //Set powerup variable to true
            refPlayer.audSource.PlayOneShot(obtainSFX); //Play obtain SFX with player's audio source
        }
        Destroy(gameObject);
    }

}
