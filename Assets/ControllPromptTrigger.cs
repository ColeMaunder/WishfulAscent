using UnityEngine;

public class ControllPromptTrigger : MonoBehaviour
{
    ControllsPrompts controllsPrompts;
    [SerializeField] int controllID;
    void Start()
    {
        controllsPrompts = GameObject.Find("Controlls Prompts").GetComponent<ControllsPrompts>();
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject enterObject = other.gameObject;
        if (enterObject.GetComponent<CharacterController>() != null){
            controllsPrompts.TiggerControll(controllID);
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
