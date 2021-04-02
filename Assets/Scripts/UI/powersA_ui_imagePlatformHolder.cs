using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_imagePlatformHolder : MonoBehaviour
{
    public RectTransform rectPosition;
    [Space(10)]
    [Tooltip("This is the height button size")]
    public float normalAnchorPosition;
    public float androidAnchorPosition;

    
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.Android) rectPosition.anchoredPosition = new Vector2(rectPosition.anchoredPosition.x, androidAnchorPosition); //set position for image on android
        else rectPosition.anchoredPosition = new Vector2(rectPosition.anchoredPosition.x, normalAnchorPosition); //set position for image on other platforms
    }
}
