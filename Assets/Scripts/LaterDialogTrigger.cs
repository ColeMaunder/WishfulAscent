using UnityEngine;
using UnityEngine.SceneManagement;

public class LaterDialogTrigger : MonoBehaviour
{
    [SerializeField] float wateTime = 5;
    [SerializeField] bool characterDependent = false;
    [SerializeField] string sequence;
    [SerializeField] int roomProgress = 1;
    [SerializeField] bool triggered;
    string name = "";
    string scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        //scene = "LevelOne";
    }
    private void OnTriggerEnter(Collider other) {
        if(!triggered){
            triggered = true;
            GameObject enterObject = other.gameObject;
            if (enterObject.GetComponent<CharacterController>() != null) {
                name = enterObject.name;
                Invoke("ToolongDialog", wateTime);
            }
        }
        

    }
    void ToolongDialog(){
        if(Saving.activeSave.roomPrgress < roomProgress){
            string nameSequence = sequence;
            if (characterDependent) {
                nameSequence += " " + name;
            }
            DialogManiger.Dialog.RunSequence(nameSequence, sequence);
        }
        gameObject.SetActive(false);
    }
}
