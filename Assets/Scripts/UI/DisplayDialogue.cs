using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class DisplayDialogue : MonoBehaviour
{
    [SerializeField]
    Transform DialogueUI;
    [SerializeField]
    float typeSpeed = 0.01f;
    DialogManiger dialogue;
    FPController controller;
    bool isTalking = false;
    //Image icon;
    TMP_Text nameDisplay;
    TMP_Text lineDisplay;
    [SerializeField]
    private bool talking = false;
    void Start()
    {
        dialogue = GameObject.FindWithTag("Managers").gameObject.GetComponent<DialogManiger>();
        controller = GameObject.FindWithTag("Player").gameObject.GetComponent<FPController>();
        nameDisplay = DialogueUI.GetChild(2).gameObject.GetComponent<TMP_Text>();
        lineDisplay = DialogueUI.GetChild(3).gameObject.GetComponent<TMP_Text>();
        //icon = DialogueUI.GetChild(1).gameObject.GetComponent<Image>();
            StartCoroutine(activeCharicterDialogLine(2, true));
    }
    
    public IEnumerator setDialogLine(string charicter, int lineID) {
        talking = true;
        DialogueUI.gameObject.SetActive(true);
        nameDisplay.text = charicter;
        DialogueLine dialog = dialogue.GetDialogue(SceneManager.GetActiveScene().name, charicter, lineID);
        lineDisplay.text = "";
        foreach (char letter in dialog.text.ToCharArray()) {
            lineDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        talking = false;
    }
    public IEnumerator activeCharicterDialogLine(int lineID, bool active)
    {
        talking = true;
        DialogueUI.gameObject.SetActive(true);
        string charicter = controller.GetActiveName(active);
        nameDisplay.text = charicter;
        DialogueLine dialog = dialogue.GetDialogue(SceneManager.GetActiveScene().name, charicter, lineID);
        lineDisplay.text = "";
        //icon.sprite = dialog.icon;
        foreach (char letter in dialog.text.ToCharArray())
        {
            lineDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        talking = false;
    }
    public bool GetTalking(){
        return talking;
    }
    public void setDialog(bool state)
    {
        DialogueUI.gameObject.SetActive(state);
    }
}
