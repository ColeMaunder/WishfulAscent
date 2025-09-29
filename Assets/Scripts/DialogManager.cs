using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class DialogManiger : MonoBehaviour {
    private DialogueDataBase dataBase;
    private Dictionary<(string scene, string name, int id), DialogueLine> dialogueLookup;
    //string filePath = Application.dataPath + "/Resources/dialogue.json";
    void Start()
    {
        //string json = System.IO.File.ReadAllText(filePath);
        TextAsset jsonFile = Resources.Load<TextAsset>("dialogue");
        dataBase = JsonUtility.FromJson<DialogueDataBase>(jsonFile.text);
        //dataBase = JsonUtility.FromJson<DialogueDataBase>(json);

        dialogueLookup = new Dictionary<(string, string, int), DialogueLine> ();
        foreach (var scene in dataBase.scenes) {
            foreach (var character in scene.characters){
               foreach (var line in character.lines){
                    dialogueLookup[(scene.scene, character.name, line.id)] = line;
                }  
            } 
        }
        
        Debug.Log(GetDialogue("Tutorial", "Luna", 2).text);
    }
    public DialogueLine GetDialogue(string sceneName, string character, int id){
        if (dialogueLookup.TryGetValue((sceneName, character, id), out DialogueLine line)) {
            return line;
        }else{
            return null;
        }
    }

    /*public void RetrieveLine()
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            DialogueLine line = JsonUtility.FromJson<DialogueLine>(json);
            print(json);
        }
        else
        {
            Debug.LogWarning("Game data file not found!");
            return;
        }
    }*/
}


[System.Serializable]
public class DialogueLine {
    public int id;
    public string icon;
    public string text;
    public string voiceOver;
    
    /*public void GetIcon(){
        
    }
    public AudioClip GetVoiceOver(){
        return 
    }*/
}

[System.Serializable]
public class CharacterDialogue {
    public string name;
    public DialogueLine[] lines;
}

[System.Serializable]
public class SceneDialogue {
    
    public string scene;
    public CharacterDialogue[] characters;
}

[System.Serializable]
public class DialogueDataBase {
    public SceneDialogue[] scenes;
}