using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


[System.Serializable]
public class SaveData {
    public string currentScene;
    public string whenSaved;
    public float playTime;
    public int roomPrgress = 0;
}

public class Saving: MonoBehaviour {
    public static SaveData activeSave = new SaveData();
    public static Saving saver = new Saving();
    private readonly string saveHeader = "Save";
    private readonly string fielName = "WishfullAscentSaves/";
    private List<string> fileDirectory = new List<string>();
    public void instantiate() {
        saver = this;
    }
    void Start()
    {
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, fielName));
        fileDirectory.AddRange(Directory.GetFiles(Path.Combine(Application.persistentDataPath, fielName)));
        
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
        int saveId = fileDirectory.Count + 1;
        string filePath = fielName + saveHeader + saveId + ".json";
        activeSave.whenSaved = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        activeSave.currentScene = "IntroCutsene";
        string json = JsonUtility.ToJson(activeSave, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filePath), json);
        fileDirectory.Add(Path.Combine(Application.persistentDataPath, filePath));
        PlayerPrefs.SetInt("MostRecentSave", saveId);
    }
    public bool AreExistingSaves() {
        string filePath = fielName + saveHeader + 1 + ".json";
        Debug.Log("Save folder Exists");
        return File.Exists(Path.Combine(Application.persistentDataPath, filePath));
        
    }
    public void SetSaveID(int newId){
        PlayerPrefs.SetInt("MostRecentSave", newId);
    }
    public void LoadSave(){
        LoadSave(PlayerPrefs.GetInt("MostRecentSave"));
        Debug.Log(PlayerPrefs.GetInt("MostRecentSave"));
    }
}
