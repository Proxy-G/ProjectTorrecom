using UnityEngine;

public class powersA_ui_platformUIHider : MonoBehaviour
{
    [Tooltip("Will this item be visible on PC?")]
    public bool visibleOnPC = true;
    [Tooltip("Will this item be visible on Android?")]
    public bool visibleOnAndroid = true;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) { //Perform these on Android
            if (visibleOnAndroid) gameObject.SetActive(true);
            else gameObject.SetActive(false);
        }
        else { //Perform these on PC
            if (visibleOnPC) gameObject.SetActive(true);
            else gameObject.SetActive(false);
        }
    }
}
