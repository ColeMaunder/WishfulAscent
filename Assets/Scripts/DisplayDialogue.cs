using UnityEngine;

public class DisplayDialogue : MonoBehaviour
{
    [SerializeField]
    GameObject DialogueUI;
    DialogManiger dialogue;
    FPController controller;
    bool isTalking = false;
    void Start()
    {
        dialogue = GameObject.FindWithTag("Managers").gameObject.GetComponent<DialogManiger>();
        controller = GameObject.FindWithTag("Player").gameObject.GetComponent<FPController>();
    }
    public void ActiveCharicterDialog(int id) {
        DialogueUI.SetActive(true);
        
    }
    public void NonActiveCharicterDialog(int id) {
        DialogueUI.SetActive(true);
        
    }
    public void SetCharicterDialog()
    {
        DialogueUI.SetActive(true);

    }
    public void setTalking(bool state){
        DialogueUI.SetActive(state);
    }
}
