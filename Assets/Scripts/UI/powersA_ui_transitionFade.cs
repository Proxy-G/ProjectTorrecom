using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powersA_ui_transitionFade : MonoBehaviour
{
    [Tooltip("What alpha should the transition fade target.")]
    public float targetAlpha = 0;
    [HideInInspector]
    public CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = targetAlpha; //Set current alpha to target alpha
    }

    // Update is called once per frame
    void Update()
    {
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.unscaledDeltaTime*20); //Set the canvas group alpha to slide towards target alpha

        //Clamp canvas group alpha if close
        if (canvasGroup.alpha < 0.01f) canvasGroup.alpha = 0;
        else if (canvasGroup.alpha > 0.99f) canvasGroup.alpha = 1;
    }

    //This function is used to change the target alpha
    public void ChangeTargetAlpha(float newAlpha)
    {
        targetAlpha = newAlpha; //Change target alpha to the new target alpha
    }
}
