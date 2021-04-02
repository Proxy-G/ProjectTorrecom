using UnityEngine;

public class powersA_game_powerup : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;
    [Tooltip("Assign the powerup obtain SFX here.")]
    public AudioClip obtainSFX;


    private powersA_game_playerMovement refPlayer;
    private powersA_game_chunkSystem chunkSystem;

    private void Start()
    {
        if (!parentChunk) GetComponentInParent<powersA_game_chunk>(); //If parent chunk wasn't assigned, detect it automatically
        if (!parentChunk) chunkSystem = GetComponentInParent<powersA_game_chunkSystem>(); //If no parent chunk can be found, try to find a parent chunk system
    }

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
