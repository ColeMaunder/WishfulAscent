using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectHandler : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData){
        if(gameObject.GetComponent<Selectable>().interactable){
            FindFirstObjectByType<EventSystem>().SetSelectedGameObject(gameObject);
        }
    }
}
