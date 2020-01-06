using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This utility script is used to handle simple sceneloading and navigation
/// Created by: Adam Chandler
/// Some of it is just simple wrappers, but it's nice to consolidate
/// </summary>
public static class SceneLoader
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public static void LoadNextScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentBuildIndex + 1);
    }

    public static void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void QuitApplication()
    {
        Application.Quit();
    }
}
