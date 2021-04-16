using UnityEngine;

public class powersA_game_missileSpawner : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to assign the parent chunk for the missle.")]
    public powersA_game_chunk parentChunk;
    [Tooltip("Assign the missile obstacle to spawn. Make sure it has an obstacle script assigned to it.")]
    public GameObject missileObject;

    void Start()
    {
        int heightDecider = Random.Range(0, 3);
        GameObject spawnedMissile;

        if (heightDecider == 0) spawnedMissile = Instantiate(missileObject, new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z), Quaternion.identity); //set height so player can jump or crouch under it
        else if (heightDecider == 1) spawnedMissile = Instantiate(missileObject, new Vector3(transform.position.x, transform.position.y + .45f, transform.position.z), Quaternion.identity); //set height so player has to jump over it 
        else spawnedMissile = Instantiate(missileObject, new Vector3(transform.position.x, transform.position.y + 1.9f, transform.position.z), Quaternion.identity); //set height so player has to crouch under it 

        spawnedMissile.transform.parent = parentChunk.transform; //set missile as child of chunk
        spawnedMissile.GetComponent<powersA_game_obstacle>().parentChunk = parentChunk; //set parent chunk for obstacle

        Destroy(gameObject); //destroy the missile spawner
    }
}
