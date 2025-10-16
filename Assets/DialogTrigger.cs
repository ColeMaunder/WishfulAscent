using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] bool characterDependent = false;
    [SerializeField] string sequence;
    string scene;
    GameObject dialogDiplay;
    void Start()
    {
        //scene = SceneManager.GetActiveScene().name;
        scene = "LevelOne";
        dialogDiplay = GameObject.Find("Dialouge");
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;
        if(enterObject.GetComponent<CharacterController>() != null){
            dialogDiplay.SetActive(true);
            if (characterDependent){
                sequence += " " + enterObject.name;
            }
            DialogManiger.Dialog.RunSequence(scene, sequence, dialogDiplay);
        }
    }
}
