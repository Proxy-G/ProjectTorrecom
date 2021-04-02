using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_UIMover : MonoBehaviour
{
    public Vector2 targetPos;
    public float percentAfter1Second = 0.01f;

    [HideInInspector]
    public RectTransform rectPosition;

    private void Start()
    {
        rectPosition = GetComponent<RectTransform>(); //get rect transform
        
        targetPos = rectPosition.anchoredPosition; //set initial target position to where it is currently
    }

    // Update is called once per frame
    void Update()
    {
        //slide rect transform to new position
        rectPosition.anchoredPosition = powersA_util_animMath.Slide(rectPosition.anchoredPosition, targetPos, percentAfter1Second);
    }

    public void ChangeTargetPosX(float newTargetPosX)
    {
        targetPos.x = newTargetPosX;
    }

    public void ChangeTargetPosY(float newTargetPosY)
    {
        targetPos.y = newTargetPosY;
    }
}
