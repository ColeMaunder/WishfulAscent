using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


[System.Serializable]
public class SaveData {
    public int saveID;
    public string currentScene;
    public string whenSaved;
    public float playTime;
    public int roomPrgress = 0;
}

public class Saving: MonoBehaviour {
    public static SaveData activeSave = new SaveData();
    public static Saving saver { get; private set; }
    private readonly string saveHeader = "Save";
    private readonly string fielName = "WishfullAscentSaves/";
    public List<SaveData> saveList = new List<SaveData>();
    public void instantiate() {
        saver = this;
    }
    void Start()
    {
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, fielName));
        foreach (var item in Directory.GetFiles(Path.Combine(Application.persistentDataPath, fielName))) {
            string[] fileName = item.Split('/');
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, fielName + fileName[fileName.Length - 1]));
            saveList.Add(JsonUtility.FromJson<SaveData>(json));
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "StartScreen"){
            activeSave.playTime += Time.deltaTime;
        }
    }
    public void PerformSave(string sceneName, int roomPrgress = 0)
    {
        string filePath = fielName + saveHeader + PlayerPrefs.GetInt("MostRecentSave") + ".json";
        activeSave.whenSaved = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        activeSave.currentScene = sceneName;
        activeSave.roomPrgress = roomPrgress;
        string json = JsonUtility.ToJson(activeSave, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filePath), json);
    }
    public void PerformSave(){
        string filePath = fielName + saveHeader + PlayerPrefs.GetInt("MostRecentSave") + ".json";
        activeSave.whenSaved = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        string json = JsonUtility.ToJson(activeSave, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filePath), json);
    }
    
    
    public void LoadSave(int newSaveId) {
        string filePath = fielName + saveHeader + newSaveId + ".json";
        if (File.Exists(Path.Combine(Application.persistentDataPath, filePath))) {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, filePath));
            activeSave = JsonUtility.FromJson<SaveData>(json);
            print(json);
            PlayerPrefs.SetInt("MostRecentSave", newSaveId);
            SceneChanger.ChangeScene.GoToScene(activeSave.currentScene);
        } else {
            Debug.LogWarning("Game data file not found!");
            return;
        }
    }
    public void newSaveFile() {
        int saveId = saveList.Count + 1;
        string filePath = fielName + saveHeader + saveId + ".json";
        activeSave.saveID = saveId;
        activeSave.playTime = 0;
        activeSave.whenSaved = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        activeSave.currentScene = "IntroCutsene";
        string json = JsonUtility.ToJson(activeSave, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filePath), json);
        saveList.Add(activeSave);
        PlayerPrefs.SetInt("MostRecentSave", saveId);
    }
    public bool AreExistingSaves() {
        string filePath = fielName + saveHeader + 1 + ".json";
        Debug.Log("Save folder Exists");
        return File.Exists(Path.Combine(Application.persistentDataPath, filePath));
        
    }
    public void LoadSave(){
        LoadSave(PlayerPrefs.GetInt("MostRecentSave"));
        Debug.Log(PlayerPrefs.GetInt("MostRecentSave"));
    }
}
