using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettinsSalectHandler : MonoBehaviour
{
    [SerializeField]
    GameObject showScreen;
    bool activated = false;

    void Update() {
        bool state = activated || EventSystem.current.currentSelectedGameObject == transform.gameObject;
        showScreen.SetActive(state);

    }
    public void SetActivated(bool state){
        activated = state;
    }
}
