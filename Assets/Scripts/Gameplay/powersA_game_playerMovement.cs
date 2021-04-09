using UnityEngine;

public class powersA_game_playerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController charController; //This is detected automatically. Used for player movements.
    private powersA_game_inputMaster inputController; //This is detected automatically. This script is used to detect inputs.

    [Tooltip("Determines intensity of players jump.")]
    public float jumpSpeed = 8.0f;
    [Tooltip("Determines gravity intensity. Unity's default is -9.81.")]
    public float gravity = -9.81f;
    public Transform testSprite;

    [HideInInspector]
    public AudioSource audSource;
    [Tooltip("Assign the player jump SFX here.")]
    public AudioClip jumpSFX;
    [Tooltip("Assign the player land SFX here.")]
    public AudioClip landSFX;
    [Tooltip("Assign the player slide SFX here.")]
    public AudioClip slideSFX;

    private Vector3 moveDirection = Vector3.zero; //Used to hold the player movement for the next frame.
    private float yDirection = 0;
    private float charHeight = 2;

    private float isGrounded; //used to detect if player is on ground
    private float isGroundedLast; //used to detect player's grounded variable last frame
    [HideInInspector]
    public float slideTimer = 0; //used to determine time remaining on slide, and if slide is occurring
    private float actionWaitTimer = 0.1f; //used to determine time remaining before player can perform another action. used to prevent button spamming.

    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool disabled = false;
    [HideInInspector]
    public bool inGap = false;
    [HideInInspector]
    public bool secChanceObtained = false;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        inputController = GetComponent<powersA_game_inputMaster>();
        audSource = GetComponent<AudioSource>();

        isDead = false; //set isDead to false once the game starts.
    }

    // Update is called once per frame
    void Update()
    {
        if (inGap && transform.position.y > -14) Physics.IgnoreLayerCollision(0, 3, true);
        else Physics.IgnoreLayerCollision(0, 3, false);

        if (!isDead) //Only allow the player script to keep working if the player is alive and not disabled
        {
            if (inputController.pause && !disabled) Time.timeScale = 0; //Check to see if player paused

            //Check if player is on ground
            if (Physics.SphereCast(new Ray(new Vector3(transform.position.x, transform.position.y + charHeight / 2, transform.position.z), Vector3.down), charController.radius * 0.99f, charHeight / 2 - charController.radius * 0.8f, 1, QueryTriggerInteraction.Ignore)) isGrounded = 0.05f;
            else isGrounded -= Time.deltaTime;
            isGrounded = Mathf.Clamp(isGrounded, 0, .05f);

            SlideCalc();
            JumpCalc();

            //Move the player and set the player's height
            charController.Move(moveDirection * Time.deltaTime);
            charController.height = charHeight;
            charController.center = new Vector3(charController.center.x, charHeight / 2, charController.center.z);

            if (slideTimer == 0 && isGrounded != 0) actionWaitTimer -= Time.deltaTime; //Countdown action wait timer
            else actionWaitTimer = 0.15f;
            actionWaitTimer = Mathf.Clamp(actionWaitTimer, 0, 0.15f);

            isGroundedLast = isGrounded; //Set is grounded from this frame for the next frame.
        }
    }

    void SlideCalc()
    {
        slideTimer -= Time.deltaTime; //Countdown slide timer
        slideTimer = Mathf.Clamp(slideTimer, 0, 0.7f); //Clamp slide timer

        //If player is grounded, pressed slide, and is not currently sliding, then start slide
        if (isGrounded != 0 && inputController.slide && slideTimer == 0 && actionWaitTimer == 0 && !disabled)
        {
            slideTimer = 0.7f;
            audSource.PlayOneShot(slideSFX); //Play slide SFX
        }

        if (slideTimer != 0) charHeight = Mathf.Lerp(charHeight, 0.99f, Time.deltaTime*20);
        else charHeight = Mathf.Lerp(charHeight, 2, Time.deltaTime*20);

        testSprite.localPosition = new Vector3(testSprite.localPosition.x, charHeight/2 - 0.075f, testSprite.localPosition.z);
        testSprite.localScale = new Vector3(testSprite.localScale.x, charHeight, testSprite.localScale.y);
    }

    void JumpCalc()
    {
        //If char is grounded and hasn't fallen in gap
        if (isGrounded != 0 && !inGap)
        {
            if (inputController.jump && slideTimer == 0 && actionWaitTimer == 0 && !disabled)
            {
                yDirection = jumpSpeed; //If player is grounded and player is pressing jump, jump
                isGrounded = 0; //Set grounded to 0 since player is jumping.

                audSource.PlayOneShot(jumpSFX); //Play jump SFX
            }
            else
            {
                if(isGrounded != 0 && isGroundedLast == 0) audSource.PlayOneShot(landSFX); //If player has just landed, play land SFX
                yDirection = 0f;
            }
        }
        else yDirection += gravity * Time.deltaTime; //If player is not grounded, apply gravity.
        Mathf.Clamp(yDirection, -60, 60); //Clamp y-direction to keep crazy values from happening.
        moveDirection.y = yDirection; //Set yDirection.
    }
}
