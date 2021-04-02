using System;
using UnityEngine;

public class powersA_game_inputMaster : MonoBehaviour
{
    InputMaster controls; //This is used to find out what buttons refer to what

    [HideInInspector]
    public bool jump;
    [HideInInspector]
    public bool slide;
    [HideInInspector]
    public bool pause;
    [HideInInspector]
    public bool confirm;
    [HideInInspector]
    public bool cancel;

    private bool jumpButtonPress = false;
    private bool slideButtonPress = false;

    [Tooltip("How far the finger must swipe for a swipe to be registered.")]
    public float swipeRange;
    [Tooltip("How long the user has to perform the swipe before cancelled.")]
    public float swipeTimer = 0.5f;


    private void Awake()
    {
        controls = new InputMaster();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale != 0) //If game is not paused.
        {
            //Collect the inputs. This is used by other scripts to refer to actions.
            if (jumpButtonPress) jump = true;
            else jump = Convert.ToBoolean(controls.Gameplay.Jump.ReadValue<float>());
            if (slideButtonPress) slide = true;
            else slide = Convert.ToBoolean(controls.Gameplay.Slide.ReadValue<float>());
            pause = Convert.ToBoolean(controls.Gameplay.Pause.ReadValue<float>());

            //reset buttons
            jumpButtonPress = false;
            slideButtonPress = false;
        }
        else
        {
            //Collect the inputs. This is used by other scripts to refer to actions.
            confirm = Convert.ToBoolean(controls.UI.Confirm.ReadValue<float>());
            cancel = Convert.ToBoolean(controls.UI.Cancel.ReadValue<float>());

            pause = false; //set pause to false to prevent endless pause loop
        }

    }

    public void JumpButton()
    {
        jumpButtonPress = true; //set jump to true
    }

    public void SlideButton()
    {
        slideButtonPress = true; //set slide to true
    }
}
