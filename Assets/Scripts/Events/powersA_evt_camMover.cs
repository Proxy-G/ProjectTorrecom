using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_evt_camMover : MonoBehaviour
{
    public Vector3 targetPos;
    public Vector3 targetRot;
    public float percentAfter1Second = 0.01f;

    [HideInInspector]
    public bool trackChunkSystem = false; //If this is true, the camera will move alongside the chunk system to give the illusion of the camera stopping
    [HideInInspector]
    public float chunkSystemSpeed = 0; //The speed of the chunk system
    [HideInInspector]
    public float gameManagerTime = 0; //The time speed of the game manager

    [HideInInspector]
    public float shakeIntensity = 0; //Shake intensity
    [HideInInspector]
    public bool playerDead = false; //Is the player dead?

    private void Start()
    {
        targetPos = transform.position; //Set initial target position to where it is currently
        targetRot = transform.eulerAngles; //Set initial target rotation to where it is currently
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDead) //Only move the camera if the player is not dead
        {
            if (!trackChunkSystem)
            {
                //Slide object to new position and rotation
                transform.position = powersA_util_animMath.Slide(transform.position, targetPos, percentAfter1Second);
                transform.rotation = powersA_util_animMath.Slide(transform.rotation, Quaternion.Euler(targetRot), percentAfter1Second);
            }
            else
            {
                //Move camera with chunk system
                transform.position = new Vector3(transform.position.x - (chunkSystemSpeed * gameManagerTime * Time.deltaTime), transform.position.y, transform.position.z);
                transform.rotation = powersA_util_animMath.Slide(transform.rotation, Quaternion.Euler(targetRot), percentAfter1Second);
            }
        }

        ShakeCamera();
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

    public void Shake(float intensity)
    {
        shakeIntensity += intensity;
        shakeIntensity = Mathf.Clamp(shakeIntensity, 0, 7);
    }

    private void ShakeCamera()
    {
        if (shakeIntensity < 0) shakeIntensity = 0;
        if (shakeIntensity > 0) shakeIntensity -= Time.deltaTime * (4 * shakeIntensity);
        else return; //shake intensity is 0, do nothing.

        Quaternion targetRot = powersA_util_animMath.Lerp(Random.rotation, Quaternion.identity, .999f);

        transform.rotation = Quaternion.Lerp(transform.rotation, Random.rotation, 0.001f * shakeIntensity);
    }
}
