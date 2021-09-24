using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class powersA_util_googlePlayManager : MonoBehaviour
{
    #if UNITY_ANDROID
    // Start is called before the first frame update
    void Start()
    {


        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            // handle results
        });
    }

    // Update is called once per frame
    void Update()
    {
        
        var config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        // Authenticate and register a ProcessAuthentication callback
        // This call needs to be made before we can proceed to other calls in the Social API
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication(bool success)
    {
        if (success)
        {
            Debug.Log("Authenticated.");
        }
        else Debug.Log("Failed to authenticate");
    }

    #endif
}
