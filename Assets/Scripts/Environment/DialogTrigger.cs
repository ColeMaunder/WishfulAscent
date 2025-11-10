using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using System;
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] bool characterDependent = false;
    [SerializeField] string sequence;
    [SerializeField] int roomProgress = 0;
    [SerializeField] bool waitForFinish = false;
    public static List<string[]> dialogQue = new List<string[]>();
    static bool dialogQueRunning = false;

    string scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        //scene = "LevelOne";
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;
        Debug.Log(enterObject.name + " entered trigger");
        if (enterObject.GetComponent<CharacterController>() != null) {
            string nameSequence = sequence;
            if (characterDependent) {
                nameSequence += " " + enterObject.name;
            }
            TriggerDialogQue(nameSequence);
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    /*IEnumerator TriggerDialog(string sequence)
    {

        yield return new WaitWhile(() => waitForFinish && DialogManiger.Dialog.GetDialogPlaying());
        if (roomProgress == 0) {
            DialogManiger.Dialog.RunSequence(scene, sequence);
        } else {
            DialogManiger.Dialog.RunSequence(scene, sequence, roomProgress);
        }
    }*/
    void TriggerDialogQue(string sequence) {
        dialogQue.Add(new string[] { scene, sequence, "" + roomProgress });
        if(!dialogQueRunning) {
            StartCoroutine(PlayDialogQue());
        } else {
            gameObject.SetActive(false);
        }
        
    }
    IEnumerator PlayDialogQue() {
        dialogQueRunning = true;
        int queLength = dialogQue.Count;
        int count = 0;
        while (count< queLength){
            yield return new WaitWhile(() => waitForFinish && DialogManiger.Dialog.GetDialogPlaying());
            string scene = dialogQue[count][0];
            string sequence = dialogQue[count][1];
            int roomProgress = int.Parse(dialogQue[count][2]);
            if (roomProgress == 0) {
                DialogManiger.Dialog.RunSequence(scene, sequence);
            } else {
                DialogManiger.Dialog.RunSequence(scene, sequence, roomProgress);
            }
            count++;
            queLength = dialogQue.Count;
        }
        dialogQue.Clear();
        dialogQueRunning = false;
        gameObject.SetActive(false);
    }
    
}
