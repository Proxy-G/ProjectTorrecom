using UnityEngine;
using UnityEngine.SceneManagement;

public class powersA_util_sceneFunctions : MonoBehaviour
{
    AsyncOperation asyncOperation;

    public void TimeScaleSet(float setTime)
    {
        Time.timeScale = setTime;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //load up the current scene (reload/restart it)
    }

    public void LoadNewLevel(Scene scene)
    {
        SceneManager.LoadScene(scene.name); //load the scene
    }

    public void StartLoadLevelAsync(Scene scene)
    {
        //Start loading a level in async. Allows game to continue playing like normal.
        asyncOperation = SceneManager.LoadSceneAsync(scene.name);
        asyncOperation.allowSceneActivation = false; //prevent scene from automatically starting
    }

    public bool CheckIsAsyncLoadIsDone()
    {
        return asyncOperation.isDone; //check if async operation is finished.
    }

    public void AllowAsyncLevelToStart()
    {
        asyncOperation.allowSceneActivation = true; //allow new scene to start
    }

    public void QuitGame()
    {
        Application.Quit(); //exit the game
    }
}
