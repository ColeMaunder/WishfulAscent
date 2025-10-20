using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettinsSalectHandler : MonoBehaviour
{
    [SerializeField]
    GameObject showScreen;
    bool activated = false;

    void Update() {
        if(activated || EventSystem.current.currentSelectedGameObject == gameObject){
            gameObject.SetActive(true);
        } else {
            showScreen.SetActive(false);
        }
    }
    public void SetActivated(bool state){
        activated = state;
    }
}
