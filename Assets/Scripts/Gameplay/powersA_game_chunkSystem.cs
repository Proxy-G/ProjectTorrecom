using System.Collections.Generic;
using UnityEngine;

public class powersA_game_chunkSystem : MonoBehaviour
{
    [Header("Chunk System Name")]
    public string systemName;

    [Space(10)]
    
    [Header("Scroll Settings")]
    [Tooltip("Once the end of a rooftop chunk reaches this x-value, the system will spawn a new chunk in.")]
    public float spawnThreshold = 20;
    [Tooltip("Once the end of a rooftop chunk reaches this x-value, the system will despawn it and remove it from the list of current chunks.")]
    public float despawnThreshold = -20;

    [Tooltip("Determine chunks movement speed.")]
    public float chunkMoveSpeed = 10;
    [Tooltip("Where on the z-axis should this system be spawning chunks?")]
    public float zDepth = 0;

    [Space(10)]

    [Header("Chunk Lists")]
    [Tooltip("The default chunk is spawned at the start of the game so the player has initial ground to start on.")]
    public GameObject defaultChunk;
    [Tooltip("These are the rooftop chunks available for the game to load up.")]
    public List<GameObject> rooftopChunks = new List<GameObject>();
    [Tooltip("These are the factory chunks available for the game to load up.")]
    public List<GameObject> factoryChunks = new List<GameObject>();
    [Tooltip("These are the street chunks available for the game to load up.")]
    public List<GameObject> streetChunks = new List<GameObject>();
    [Tooltip("These are the lab chunks available for the game to load up.")]
    public List<GameObject> labChunks = new List<GameObject>();
    
    [Space(10)]

    public GameObject rooftopToFactoryChunk; //this gameobject is used in the transiton between rooftop and factory
    public GameObject factoryToStreetsChunk; //this gameobject is used in the transiton between factory and streets
    public GameObject streetsToLabChunk; //this gameobject is used in the transiton between streets and lab
    public GameObject labToRooftopChunk; //this gameobject is used in the transiton between lab and rooftop

    private List<GameObject> availChunkList; //this dictates the current list of chunks the chunk system can use.

    /// <summary>
    /// This list holds all the chunks currently loaded into the game.
    /// </summary>
    private List<powersA_game_chunk> chunkList = new List<powersA_game_chunk>();
    private powersA_game_chunk refChunk;
    private powersA_game_chunk spawnedChunk;
    private float chunkPosCheckCooldown = 0.1f; //Evert 1/10th secs, the positions of the newest and oldest chunk will be checked.

    [HideInInspector]
    public GameObject overrideChunk; //this is used to allow devs to pass in a chunk manually. just make sure it has the chunk script on it.
    [HideInInspector]
    public int currentEnvironment; //this is used to control what environment is used currently. 0 = rootfop, 1 = factory, 2 = streets, 3 = labs;
    public int lastEnvironment; //sets the environment of the latest spawned chunk. allows for transitions.

    [HideInInspector]
    public powersA_game_gameManager gameManager;
    private float envChangeTime = 20;
    private float envChangeCooldown;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<powersA_game_gameManager>();

        ResetChunkSystem();
    }

    // Update is called once per frame
    void Update()
    {
        chunkPosCheckCooldown -= Time.deltaTime; //countdown the chunk position check cooldown

        //if cooldown complete
        if (chunkPosCheckCooldown <= 0) ChunkCheck();

        envChangeCooldown -= Time.deltaTime; //countdown the time until environment changes      
    }

    void ChunkCheck()
    {
        //if the newest chunk's end point has gone past the spawn threshold, then spawn a chunk in.
        refChunk = chunkList[chunkList.Count - 1];
        if (refChunk.transform.position.x + refChunk.halfWidth < spawnThreshold)
        {
            if (envChangeCooldown < 0) currentEnvironment += 1; //if cooldown has completed, change environment.

            if (currentEnvironment == lastEnvironment) SpawnChunk(); //spawn chunk like normal if environment is still the same
            else ChangeEnvironmentChunks(); //set up environment transition
        }

        //if the oldest chunk's end point has gone past the despawn threshold, then despawn the oldest chunk.
        refChunk = chunkList[0]; //get the chunk in a variable for reference
        if (refChunk.transform.position.x+refChunk.halfWidth < despawnThreshold) DespawnChunk();

        chunkPosCheckCooldown = 0.1f;
    }

    void SpawnChunk()
    {
        //if an override chunk has been passed into the script, spawn that instead of randomly spawning one.
        if (overrideChunk) spawnedChunk = Instantiate(overrideChunk, Vector3.zero, Quaternion.identity).GetComponent<powersA_game_chunk>();
        //else spawn chunk at random and store it's chunk script in a variable
        else spawnedChunk = Instantiate(availChunkList[Random.Range(0, availChunkList.Count)], Vector3.zero, Quaternion.identity).GetComponent<powersA_game_chunk>();

        //set the new chunk's position using the last chunk's position, it's half width, and the new chunk's half width.
        spawnedChunk.transform.position = new Vector3(refChunk.transform.position.x + refChunk.halfWidth + spawnedChunk.halfWidth - (refChunk.speed * gameManager.time * Time.deltaTime), 0, zDepth);
        spawnedChunk.chunkSystem = this; //add the chunk system to the chunk's own script
        chunkList.Add(spawnedChunk); //add the chunk to the chunk list

        overrideChunk = null; //make the override chunk null to ensure an override chunk isn't repeated
        lastEnvironment = currentEnvironment; //set the last environment to the environment this chunk is from
    }
    
    void DespawnChunk()
    {
        chunkList.RemoveAt(0); //remove the chunk
        Destroy(refChunk.gameObject); //destroy the old gameobject
    }

    void ChangeEnvironmentChunks()
    {
        if (currentEnvironment == 4) currentEnvironment = 0; //if current environment value has exceeded number of environments, reset it.

        //spawn new chunk based on what environment we are going into
        if(currentEnvironment == 0) spawnedChunk = Instantiate(labToRooftopChunk, Vector3.zero, Quaternion.identity).GetComponent<powersA_game_chunk>();
        else if (currentEnvironment == 1) spawnedChunk = Instantiate(rooftopToFactoryChunk, Vector3.zero, Quaternion.identity).GetComponent<powersA_game_chunk>();
        else if(currentEnvironment == 2) spawnedChunk = Instantiate(factoryToStreetsChunk, Vector3.zero, Quaternion.identity).GetComponent<powersA_game_chunk>();
        else spawnedChunk = Instantiate(streetsToLabChunk, Vector3.zero, Quaternion.identity).GetComponent<powersA_game_chunk>();

        //set the new chunk's position using the last chunk's position, it's half width, and the new chunk's half width.
        spawnedChunk.transform.position = new Vector3(refChunk.transform.position.x + refChunk.halfWidth + spawnedChunk.halfWidth - (refChunk.speed * gameManager.time * Time.deltaTime), 0, zDepth);
        spawnedChunk.chunkSystem = this; //add the chunk system to the chunk's own script
        chunkList.Add(spawnedChunk); //add the chunk to the chunk list

        //set current chunk list dependent on environment
        if (currentEnvironment == 0) availChunkList = rooftopChunks;
        else if (currentEnvironment == 1) availChunkList = factoryChunks;
        else if (currentEnvironment == 2) availChunkList = streetChunks;
        else availChunkList = labChunks;

        envChangeTime += 10; //increase time between environments slightly
        envChangeCooldown = envChangeTime; //reset cooldown
        lastEnvironment = currentEnvironment; //reset the environment
    }

    //This is used to reset the chunk system when the player is restarting.
    public void ResetChunkSystem()
    {
        ClearChunkSystem();
        
        //Make sure there IS a default chunk. If so, spawn three in to keep player from immediately dying.
        if (defaultChunk)
        {
            GameObject chunk;
            float defaultChunkHalfWidth = defaultChunk.GetComponent<powersA_game_chunk>().halfWidth;
            float startChunkPos = despawnThreshold + defaultChunkHalfWidth;

            while (startChunkPos - defaultChunkHalfWidth < spawnThreshold) //spawn chunks until the chunks get pass the spawn threshold
            {
                chunk = Instantiate(defaultChunk, new Vector3(startChunkPos, 0, zDepth), Quaternion.identity); //spawn chunk

                powersA_game_chunk chunkScript = chunk.GetComponent<powersA_game_chunk>();
                chunkScript.chunkSystem = this; //add the chunk system to the chunk's own script
                chunkList.Add(chunkScript); //add the chunk to the chunk list
                startChunkPos += chunkScript.halfWidth * 2; //add the chunk width to the spawn position
            }
        }
        else Debug.LogError("ERROR! There is no default chunk! No chunks will be spawned at the start!!!");

        //set the environment variables
        currentEnvironment = 0;
        lastEnvironment = 0;
        availChunkList = rooftopChunks;
        envChangeCooldown = envChangeTime;
    }

    //This is used to clear the chunk system
    public void ClearChunkSystem()
    {
        for (int i = chunkList.Count - 1; i > 0; i--)
        {
            refChunk = chunkList[i]; //get the chunk to destroy it later
            chunkList.RemoveAt(i); //remove the chunk from list
            Destroy(refChunk.gameObject); //destroy the chunk
        }
    }
}
