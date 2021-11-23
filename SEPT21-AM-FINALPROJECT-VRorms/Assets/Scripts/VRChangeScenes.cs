using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRChangeScenes : MonoBehaviour
{
    private AsyncOperation loadingOperation;

    public void ChangeScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void SceneChange(string sceneName)
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
