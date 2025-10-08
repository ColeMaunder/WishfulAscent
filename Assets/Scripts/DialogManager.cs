using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
public class DialogManiger : MonoBehaviour {
    public static DialogManiger Dialog;
    public void instantiate() {
        Dialog = this;
    }
    private DialogueDataBase dataBase;
    //private string[] charicterNames = { "Luna", "Sol", "Stella" };
    private Dictionary<(string scene, string sequence, int id), DialogueLine> dialogueLookup;
    //string filePath = Application.dataPath + "/Resources/dialogue.json";
    void Start()
    {
        //string json = System.IO.File.ReadAllText(filePath);
        TextAsset jsonFile = Resources.Load<TextAsset>("dialogue Refomated");
        dataBase = JsonUtility.FromJson<DialogueDataBase>(jsonFile.text);
        //dataBase = JsonUtility.FromJson<DialogueDataBase>(json);

        dialogueLookup = new Dictionary<(string, string, int), DialogueLine> ();
        foreach (var scene in dataBase.scenes) {
            foreach (var character in scene.sequences){
               foreach (var line in character.lines){
                    dialogueLookup[(scene.scene, character.sequence, line.id)] = line;
                }  
            } 
        }
        
        Debug.Log(GetDialogue("LevelOne", "Base Mechanics Tutorial", 2 ).text);
    }
    public DialogueLine GetDialogue(string sceneName, string sequence, int id){
        if (dialogueLookup.TryGetValue((sceneName, sequence, id), out DialogueLine line)) {
            return line;
        }else{
            Debug.Log("No sutch instance in the file");
            return null;
        }
    }
    /*public DialogueLine GetDialogue(string sceneName, int id)
    {
        foreach (string name in charicterNames)
        {
            if (dialogueLookup.TryGetValue((sceneName, name, id), out DialogueLine line)) {
                line.name = name;
                return line;
            }
        }
        Debug.Log("No sutch instance in the file");
        return null;
    }*/

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
    public string name;
    public int id;
    public string text;
    public string voiceOver;
    
    
    /*public void GetIcon(){
        
    }
    public AudioClip GetVoiceOver(){
        return 
    }*/
}

[System.Serializable]
public class SequencesDialogue {
    public string sequence;
    public DialogueLine[] lines;
}

[System.Serializable]
public class SceneDialogue {
    
    public string scene;
    public SequencesDialogue[] sequences;
}

[System.Serializable]
public class DialogueDataBase {
    public SceneDialogue[] scenes;
}