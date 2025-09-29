using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonsManger : MonoBehaviour
{
    EventSystem eventSystem;
    GameObject lastSalected = null;
    [SerializeField]
    GameObject[] watchedButtons;
    void Start()
    {
       eventSystem = GameObject.FindAnyObjectByType<EventSystem>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject && lastSalected != eventSystem.currentSelectedGameObject){
            lastSalected = eventSystem.currentSelectedGameObject;
        }
        if (!eventSystem.currentSelectedGameObject && lastSalected) {
            eventSystem.SetSelectedGameObject(lastSalected);
        }
    }
}
