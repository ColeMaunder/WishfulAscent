using System;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField]
    String nextSceen;
    SceneChanger changer;
    void Start()
    {
        changer = GameObject.FindWithTag("Managers").GetComponent<SceneChanger>();
    }
    public void continueGame(){
        changer.GoToScene(nextSceen);
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
