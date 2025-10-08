using System;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField]
    String nextSceen;

    public void continueGame(){
        SceneChanger.ChangeScene.GoToScene(nextSceen);
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
