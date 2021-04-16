using UnityEngine;

public class powersA_game_obstRandomSpawner : MonoBehaviour
{
    [Tooltip("Assign the chunk this obstacle is in. This is used to assign the parent chunk for the missle.")]
    public powersA_game_chunk parentChunk;
    [Tooltip("Assign the ground obstacle to spawn. Make sure it has an obstacle script assigned to it.")]
    public GameObject groundObject;
    [Tooltip("Assign the sky obstacle to spawn. Make sure it has an obstacle script assigned to it.")]
    public GameObject skyObject;

    void Start()
    {
        bool isGroundObj = (Random.value > 0.5f);
        GameObject spawnedObst;

        if (isGroundObj) spawnedObst = Instantiate(groundObject, new Vector3(transform.position.x, transform.position.y +.5f, transform.position.z), Quaternion.identity); //Set height so player can jump or crouch under it
        else spawnedObst = Instantiate(skyObject, new Vector3(transform.position.x, skyObject.transform.position.y, transform.position.z), Quaternion.identity); //Set height so player has to crouch under it 

        spawnedObst.transform.parent = parentChunk.transform; //Set missile as child of chunk
        
        spawnedObst.GetComponent<powersA_game_obstacle>().parentChunk = parentChunk; //Set parent chunk for obstacle

        Destroy(gameObject); //Destroy the missile spawner
    }
}
