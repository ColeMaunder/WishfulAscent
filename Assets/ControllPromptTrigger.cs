using UnityEngine;

public class ControllPromptTrigger : MonoBehaviour
{
    ControllsPrompts controllsPrompts;
    FPController controller;
    [SerializeField] int controllID;

    void Start()
    {
        controllsPrompts = GameObject.Find("Controlls Prompts").GetComponent<ControllsPrompts>();
        controller = FindFirstObjectByType<FPController>().GetComponent<FPController>();
    }

    private void OnTriggerStay(Collider other)
    {
        Transform enterObject = other.transform;
        if (enterObject.GetComponent<CharacterController>() != null){
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
