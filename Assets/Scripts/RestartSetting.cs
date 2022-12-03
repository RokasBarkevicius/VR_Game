using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSetting : MonoBehaviour
{
    public void RestartScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp(){
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Application has quit");
    }
}
