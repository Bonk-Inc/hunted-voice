using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Reloads the current active scene
/// </summary>
public class SceneSwitcher : MonoBehaviour {

    /// <summary>
    /// Reloads the current active scene
    /// </summary>
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void VisitUrl(string link){
        Application.OpenURL(link);
    }
}