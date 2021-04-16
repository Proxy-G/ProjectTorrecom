using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_buttonsPlatformHolder : MonoBehaviour
{
    public RectTransform rectPosition;
    public Button button;
    [Space(10)]
    [Tooltip("This is the height button size")]
    public float normalAnchorPosition;
    public float androidAnchorPosition;
    public float normalButtonSize = 0;
    public float androidButtonSize = 0;
    public Text textTargetGraphic;
    public Image imageTargetGraphic;

    
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android) //Perform these changes on android
        {
            if(rectPosition)
            {
                rectPosition.anchoredPosition = new Vector2(rectPosition.anchoredPosition.x, androidAnchorPosition); //Set position for button
                rectPosition.sizeDelta = new Vector2(rectPosition.sizeDelta.x, androidButtonSize); //Set button size for android
            }
            if(imageTargetGraphic) imageTargetGraphic.gameObject.SetActive(true); //Enable the image target graphic
            if(textTargetGraphic) textTargetGraphic.gameObject.SetActive(true); //Enable the text target graphic
            if(button) button.targetGraphic = imageTargetGraphic; //Set the graphic affected by the button to the one designated for android 
        }
        else //Perform these on PC
        {
            if(rectPosition)
            {
                rectPosition.anchoredPosition = new Vector2(rectPosition.anchoredPosition.x, normalAnchorPosition); //Set position for button
                rectPosition.sizeDelta = new Vector2(rectPosition.sizeDelta.x, normalButtonSize); //Set button size for other platforms
            }
            if(imageTargetGraphic) imageTargetGraphic.gameObject.SetActive(false); //Disable the image target graphic
            if(textTargetGraphic) textTargetGraphic.gameObject.SetActive(true); //Enable the text target graphic
            if(button) button.targetGraphic = textTargetGraphic; //Set the graphic affected by the button to the one designated for other platforms 
        }

    }
}
