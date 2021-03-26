using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_objectMover : MonoBehaviour
{
    public Vector3 targetPos;
    public float percentAfter1Second = 0.01f;

    private void Start()
    {
        targetPos = transform.position; //set initial target position to where it is currently
    }

    // Update is called once per frame
    void Update()
    {
        //slide object to new position
        transform.position = powersA_util_animMath.Slide(transform.position, targetPos, percentAfter1Second);
    }

    public void ChangeTargetPosX(float newTargetPosX)
    {
        targetPos.x = newTargetPosX;
    }
    public void ChangeTargetPosY(float newTargetPosY)
    {
        targetPos.y = newTargetPosY;
    }
    public void ChangeTargetPosZ(float newTargetPosZ)
    {
        targetPos.z = newTargetPosZ;
    }
}
