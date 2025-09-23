using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDistroy : MonoBehaviour
{
    private static GameObject[] persistantObjects = new GameObject[4];
    public int objectID;
    public String[] isNotIn;
    void Awake()
    {
        if (persistantObjects[objectID] == null)
        {
            persistantObjects[objectID] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistantObjects[objectID] != gameObject)
        {
            Destroy(gameObject);
        }
    }
    public void OnSceneLoaded() {
        string sceneName = SceneManager.GetActiveScene().name;
        foreach(string i in isNotIn){
            if (i == sceneName){
                Destroy(gameObject);
            }
        }
    }
}
