using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class powersA_game_inputMaster : MonoBehaviour
{
    powersA_game_playerMovement player; //This is used in order to see if player is paused or not.
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

    //#if UNITY_ANDROID
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    //#endif

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
        //include this code with the unity editor or standalone
        #if UNITY_EDITOR || UNITY_STANDALONE
        if(Time.timeScale != 0) //If game is not paused.
        {
            //Collect the inputs. This is used by other scripts to refer to actions.
            jump = Convert.ToBoolean(controls.Gameplay.Jump.ReadValue<float>());
            slide = Convert.ToBoolean(controls.Gameplay.Slide.ReadValue<float>());
            pause = Convert.ToBoolean(controls.Gameplay.Pause.ReadValue<float>());
        }
        else
        {
            //Collect the inputs. This is used by other scripts to refer to actions.
            confirm = Convert.ToBoolean(controls.UI.Confirm.ReadValue<float>());
            cancel = Convert.ToBoolean(controls.UI.Cancel.ReadValue<float>());

            pause = false; //set pause to false to prevent endless pause loop
        }
#endif


#if UNITY_ANDROID         //include this code with the android version

#endif
        //if touch begins, get initial finger screen position
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began) startTouchPosition = Input.GetTouch(0).position; 
        //if player has moved finger touch, compare starting touch position with current, and perform an action is moved far enough
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position; //start getting screen position
            Vector2 dis = currentPosition - startTouchPosition; //compare screen position with

            if(!stopTouch)
            {
                if (dis.y > swipeRange) //if touch has gone up, do a jump and end touch
                {
                    jump = Convert.ToBoolean(controls.Gameplay.Jump.ReadValue<float>());
                    stopTouch = true;
                }
                else if (dis.y < -swipeRange) //if touch has gone up, do a slide and end touch
                {
                    slide = Convert.ToBoolean(controls.Gameplay.Slide.ReadValue<float>());
                    stopTouch = true;
                }
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
        {
            stopTouch = false;
        }
    }

    private void StartTouchCheck(InputAction.CallbackContext ctx)
    {

    }

    private void EndTouchCheck()
    {

    }
}
