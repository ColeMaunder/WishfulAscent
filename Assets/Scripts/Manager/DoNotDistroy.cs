using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

public class DoNotDistroy : MonoBehaviour
{
    private static GameObject[] persistantObjects = new GameObject[5];
    public int objectID;
    public String[] isNotIn;
    void Awake()
    {
        if (persistantObjects[objectID] == null)
        {
            persistantObjects[objectID] = gameObject;
            DontDestroyOnLoad(gameObject);
            foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>()) {
                MethodInfo method = script.GetType().GetMethod("instantiate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (method != null) {
                    method.Invoke(script, null);
                }
            }
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
