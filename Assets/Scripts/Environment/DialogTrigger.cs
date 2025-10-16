using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] bool characterDependent = false;
    [SerializeField] string sequence;
    string scene;
    void Start()
    {
        //scene = SceneManager.GetActiveScene().name;
        scene = "LevelOne";
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;
        Debug.Log(enterObject.name + " entered trigger");
        if(enterObject.GetComponent<CharacterController>() != null){
            string nameSequence = sequence;
            if (characterDependent){
                nameSequence += " " + enterObject.name;
            }
            DialogManiger.Dialog.RunSequence(scene, nameSequence);
        }
        gameObject.SetActive(false);
    }
}
