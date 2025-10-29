using UnityEngine;

public class ControllPromptTrigger : MonoBehaviour
{
    ControllsPrompts controllsPrompts;
    FPController controller;
    [SerializeField] int controllID;
    [SerializeField] int progressRequired;

    void Start()
    {
        controllsPrompts = GameObject.Find("Controlls Prompts").GetComponent<ControllsPrompts>();
        controller = FindFirstObjectByType<FPController>().GetComponent<FPController>();
    }
    void Update()
    {
        if(Time.timeScale <= 0){
            controllsPrompts.activateControllUI(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Transform enterObject = other.transform;
        if (Saving.activeSave.roomPrgress == progressRequired && enterObject.GetComponent<CharacterController>() != null){
            controllsPrompts.activateControllUI(true);
            if (enterObject == controller.GetActiveCharicter()){
                controllsPrompts.TriggerCamra(controller.GetActiveCharicter().name);
                controllsPrompts.TiggerControll(controllID);
            }else if (controllsPrompts.GetCamra() == "") {
                controllsPrompts.activateControllUI(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject enterObject = other.gameObject;
        if (enterObject.GetComponent<CharacterController>() != null){
            controllsPrompts.activateControllUI(false);
        }
    }
}
