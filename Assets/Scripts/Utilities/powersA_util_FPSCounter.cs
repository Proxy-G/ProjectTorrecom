using UnityEngine;
using UnityEngine.UI;

public class powersA_util_FPSCounter : MonoBehaviour
{
    public Text fpsText;
    public Text avgText;
    public Text maxText;
    public Text minText;

    private float fps = 0;
    private float avgFPS = 0;
    private float maxFPS = 0; //this is used to indicate the highest fps seen in the game
    private float minFPS = Mathf.Infinity; //this is used to indicate the lowest fps seen in the game
    private float fpsCheckCounter = 0; //this is used to indicate how many times the fps has been checked
    private float fpsTotal = 0; //this is used to accumulate all the fps numbers counted for the average

    private float fpsCheckCountdown = 0.1f;
    private float updateTextCountdown = 0.5f;

    private void Start()
    {
        //set values
        avgFPS = 0;
        minFPS = Mathf.Infinity;
        maxFPS = 0;
        fpsCheckCounter = 0;
        fpsTotal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if countdown complete, calculate the fps, avg fps, min fps, and max fps
        if (fpsCheckCountdown <= 0) FPSMath();
        else fpsCheckCountdown -= Time.deltaTime;

        //if countdown complete, calculate the fps, avg fps, min fps, and max fps
        if (updateTextCountdown <= 0) UpdateText();
        else updateTextCountdown -= Time.deltaTime;
    }

    void FPSMath()
    {
        fps = 1 / Time.unscaledDeltaTime; //get current fps

        if (fps > maxFPS) maxFPS = fps; //if current fps is higher than the max fps, set max fps to fps
        if (fps < minFPS) minFPS = fps; //if current fps is lower than the min fps, set min fps to fps

        fpsTotal += fps; //add current fps to total fps
        fpsCheckCounter++; //add to the frame counter
        avgFPS = fpsTotal / fpsCheckCounter; //divide total fps counted by the number of times fps has been counted

        //reset countdown
        fpsCheckCountdown = 0.1f;
    }

    void UpdateText()
    {
        //set the text to each value
        fpsText.text = "" + (int)fps;
        avgText.text = "" + (int)avgFPS;
        maxText.text = "" + (int)maxFPS;
        minText.text = "" + (int)minFPS;

        //reset countdown
        updateTextCountdown = .5f;
    }
}
