using System;
using System.Data.Common;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public string currentRoom;
}

public class Saving: MonoBehaviour {
    public static SaveData save = new SaveData();
    private readonly string saveHeader = "Save";
    private int saveId = 1;
    
    public void PerformSave()
    {
        string filePath = Application.persistentDataPath + "/" + saveHeader + saveId + ".json";
        string json = JsonUtility.ToJson(save, true);
        System.IO.File.WriteAllText(filePath, json);
    }
    
    public void LoadSave(int newSaveId) {
        string filePath = Application.persistentDataPath + "/" + saveHeader +  newSaveId + ".json";
        if (System.IO.File.Exists(filePath)) {
            saveId = newSaveId;
            string json = System.IO.File.ReadAllText(filePath);
            save = JsonUtility.FromJson<SaveData>(json);
            print(json);
        } else {
            Debug.LogWarning("Game data file not found!");
            return;
        }
    }
    
    public void SetSaveID(int newId){
        saveId = newId;
    }
}
