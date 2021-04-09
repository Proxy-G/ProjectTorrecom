using UnityEngine;
using UnityEngine.UI;

public class powersA_ui_missionModeUIHider : MonoBehaviour
{
    public powersA_game_gameManager gameManager;
    [Tooltip("Will this item be visible on Mission Mode?")]
    public bool visibleOnMission = true;
    [Tooltip("Will this item be visible on Endless Mode?")]
    public bool visibleOnEndless = true;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        //If in mission mode, check to see to be visible on mission
        if (gameManager.missionMode && visibleOnMission) text.enabled = true;
        else if (gameManager.missionMode && !visibleOnMission) text.enabled = false;

        //If in endless mode, check to see to be visible on endless
        if (!gameManager.missionMode && visibleOnEndless) text.enabled = true;
        else if (!gameManager.missionMode && !visibleOnEndless) text.enabled = false;
    }
}
