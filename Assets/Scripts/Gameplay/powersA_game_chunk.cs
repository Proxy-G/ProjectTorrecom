using UnityEngine;

public class powersA_game_chunk : MonoBehaviour
{
    [Tooltip("Put how wide this chunk is here. This is used by the chunk system to correctly place this item.")]
    public float halfWidth = 10;
    [Tooltip("How many units should the chunk move to the left per sec?")]
    public float speed = 10;

    [HideInInspector]
    public powersA_game_chunkSystem chunkSystem; //Reference to the chunk system. This is used for the speed multiplier on the chunk system.

    // Update is called once per frame
    void Update()
    {
        //if chunk system is valid, update speed variable to fit chunk system's
        if (chunkSystem) speed = chunkSystem.chunkMoveSpeed;
        
        //if chunk system is valid, use chunk system speed multiplier. else, move without it.
        if(chunkSystem) transform.position = new Vector3(transform.position.x - (speed * chunkSystem.gameManager.time * Time.deltaTime), transform.position.y, transform.position.z);
        else transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
    }
}
