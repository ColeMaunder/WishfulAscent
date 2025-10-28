using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] bool characterDependent = false;
    [SerializeField] string sequence;
    [SerializeField] int roomProgress = 0;
    string scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;
        Debug.Log(enterObject.name + " entered trigger");
        if(enterObject.GetComponent<CharacterController>() != null){
            string nameSequence = sequence;
            if (characterDependent)
            {
                nameSequence += " " + enterObject.name;
            }
            if(roomProgress == 0){
                DialogManiger.Dialog.RunSequence(scene, nameSequence);
            }else{
                DialogManiger.Dialog.RunSequence(scene, nameSequence,roomProgress);
            }
            
            gameObject.SetActive(false);
        }
    }
}
