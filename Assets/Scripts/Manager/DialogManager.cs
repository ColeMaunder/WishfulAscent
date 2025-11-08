using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class DialogManiger : MonoBehaviour {
    Coroutine dialougeSequence;
    Coroutine progresser;
    public static DialogManiger Dialog;
    private bool dialogPlaying;
    public void instantiate() {
        Dialog = this;
    }
    private DialogueDataBase dataBase;
    //private string[] charicterNames = { "Luna", "Sol", "Stella" };
    private Dictionary<(string scene, string sequence, int id), DialogueLine> dialogueLookup;
    //string filePath = Application.dataPath + "/Resources/dialogue.json";
    void Awake()
    {
        //string json = System.IO.File.ReadAllText(filePath);
        TextAsset jsonFile = Resources.Load<TextAsset>("Dialogue");
        dataBase = JsonUtility.FromJson<DialogueDataBase>(jsonFile.text);
        //dataBase = JsonUtility.FromJson<DialogueDataBase>(json);

        dialogueLookup = new Dictionary<(string, string, int), DialogueLine>();
        foreach (var scene in dataBase.scenes)
        {
            foreach (var character in scene.sequences)
            {
                foreach (var line in character.lines)
                {
                    dialogueLookup[(scene.scene, character.sequence, line.id)] = line;
                }
            }
        }

        //Debug.Log(GetDialogue("LevelOne", "Base Mechanics Tutorial", 2).text);
    }
    public void newScene(){

        if (dialougeSequence != null) {
            StopCoroutine(dialougeSequence);
        }
        if (progresser != null) {
            StopCoroutine(progresser);
        }
    }
    public DialogueLine GetDialogue(string sceneName, string sequence, int id) {
        if (dialogueLookup.TryGetValue((sceneName, sequence, id), out DialogueLine line)) {
            line.voiceOverAudio = Resources.Load<AudioClip>("Dialog Audio/" + sceneName + "/" + sequence + "/" + line.voiceOver);
            return line;
        } else {
            Debug.Log("No sutch instance in the file");
            return null;
        }
    }
    public void RunSequence(string scene, string sequence)
    {
        if (dialougeSequence != null) {
            StopCoroutine(dialougeSequence);
        }
        DisplayDialogue display = GameObject.Find("Ui Screnes").GetComponent<DisplayDialogue>();
        dialougeSequence = StartCoroutine(DialougeSequence(scene, sequence, display));
    }
    public void RunSequence(string scene, string sequence, int progress) {
        RunSequence(scene, sequence);
        if (progresser != null) {
            StopCoroutine(progresser);
        }
        progresser = StartCoroutine(RoomProsreser(progress));
    }
    public bool GetDialogPlaying() {
        return dialogPlaying;
    }
    private IEnumerator RoomProsreser(int progress){
        while (dialogPlaying){
            yield return new WaitForSeconds(0.5f);
        }
        Saving.activeSave.roomPrgress = progress;   
    }
    private IEnumerator DialougeSequence(string scene, string sequence, DisplayDialogue display){
        dialogPlaying = true;
        int idCount = 1;
        display.Activate(true);
        TMP_Text speakerName = display.nameDisplay;
        TMP_Text text = display.lineDisplay;
        DialogueLine line = GetDialogue(scene, sequence, idCount);
        while (line != null){
            speakerName.text = line.name;
            text.text = line.text;
            if(line.voiceOverAudio != null){
                AudioSource speeker = GameObject.Find(line.name).transform.GetComponentInChildren<AudioSource>();
                speeker.clip = line.voiceOverAudio;
                speeker.Play();
                Debug.Log("Playing Audio ID:"+ idCount);
                while (speeker.isPlaying){
                    yield return new WaitForSeconds(0.5f);
                }
            }else{
                yield return new WaitForSeconds(1);
                Debug.Log("No Audio File for ID:" + idCount);
            }
            idCount++;
            line = GetDialogue(scene, sequence, idCount);
        }
        yield return new WaitForSeconds(1);
        display.Activate(false);
        dialogPlaying = false;
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
    public AudioClip voiceOverAudio;
    
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