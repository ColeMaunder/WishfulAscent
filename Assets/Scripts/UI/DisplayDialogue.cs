using UnityEngine;
using TMPro;
public class DisplayDialogue : MonoBehaviour
{
    [SerializeField]
    Transform DialogueUI;
    [SerializeField]
    [HideInInspector]
    public TMP_Text nameDisplay;
    [HideInInspector]
    public TMP_Text lineDisplay;
    
    void Start()
    {
        nameDisplay = DialogueUI.GetChild(1).gameObject.GetComponent<TMP_Text>();
        lineDisplay = DialogueUI.GetChild(2).gameObject.GetComponent<TMP_Text>();
    }
    public void Activate(bool state){
        DialogueUI.gameObject.SetActive(state);
    }
}
    
