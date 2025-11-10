using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonsManger : MonoBehaviour
{

    GameObject lastSalected = null;
    [SerializeField]
    


    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject && lastSalected != EventSystem.current.currentSelectedGameObject &&
         EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().interactable) {
            lastSalected = EventSystem.current.currentSelectedGameObject;
        }
        try{
            if (!EventSystem.current.currentSelectedGameObject && lastSalected ||
            !EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().interactable) {
                    EventSystem.current.SetSelectedGameObject(lastSalected);
                }
        } catch (MissingReferenceException){
                Debug.Log("object not hter to be salected");
            }
    }
}
