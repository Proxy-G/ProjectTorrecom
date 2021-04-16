using UnityEngine;

public class powersA_ui_platformUIHider : MonoBehaviour
{
    public powersA_ui_UIManager uiManager;
    
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

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && visibleOnAndroid) //If on Android...
        {
            //Disable gameplay UI buttons during cutscenes and menu locks. Activate them during gameplay.
            if(uiManager.lockMenu == true) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
        else enabled = false; //If not on Android, operation of this script is complete.
    }
}
