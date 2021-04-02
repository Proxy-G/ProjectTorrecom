using UnityEngine;

public class powersA_game_obstacle : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to access the time manager.")]
    public powersA_game_chunk parentChunk;
    [Tooltip("The speed the obstacle should move through the environment at.")]
    public float speed;
    [Tooltip("Does this obstacle override the 'second chance' powerup? This is mainly used for pits the player falls into.")]
    public bool secChanceOverride = false;

    private powersA_game_playerMovement refPlayer;

    private powersA_game_chunkSystem chunkSystem;

    private void Start()
    {
        if (!parentChunk) GetComponentInParent<powersA_game_chunk>(); //If parent chunk wasn't assigned, detect it automatically
        if (!parentChunk) chunkSystem = GetComponentInParent<powersA_game_chunkSystem>(); //If no parent chunk can be found, try to find a parent chunk system
    }

    private void Update()
    {
        if (speed != 0) transform.localPosition = new Vector3(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        refPlayer = other.GetComponent<powersA_game_playerMovement>(); //Detect if the other collider is the player

        if (refPlayer && secChanceOverride || refPlayer && !refPlayer.secChanceObtained) ObstacleHit(refPlayer); //If player script is not null, perform actions
        else if (refPlayer && refPlayer.secChanceObtained)
        {
            refPlayer.secChanceObtained = false; //Remove the player's powerup
            Destroy(gameObject); //Destroy the obstacle if the player has the powerup
        }
    }

    public void ObstacleHit(powersA_game_playerMovement playerScript)
    {
        playerScript.isDead = true; //Player has died
        
        if (parentChunk) parentChunk.chunkSystem.gameManager.time = 0; //Stop the chunk system
        else if (chunkSystem) chunkSystem.gameManager.time = 0; //Stop the chunk system

        enabled = false; //Disable the obstacle to prevent more OnTriggerEnter code from running
    }
}
