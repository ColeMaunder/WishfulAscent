using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    GameObject volumeScreen;
    [SerializeField]
    GameObject controllsScreen;
    [SerializeField]
    Button [] buttons;
    public void volume(bool state){
        volumeScreen.SetActive(state);
        AllInteractible(state);
        if(state){
            EventSystem.current.SetSelectedGameObject(volumeScreen.transform.GetChild(2).gameObject);
        }else{
            EventSystem.current.SetSelectedGameObject(transform.GetChild(3).gameObject);
        }
    }
    public void controlls(bool state){
        controllsScreen.SetActive(state);
        AllInteractible(state);
        if(state){
            
            
            
        }else{
            EventSystem.current.SetSelectedGameObject(transform.GetChild(4).gameObject);
        }
    }
    private void AllInteractible(bool state) {
        foreach (var item in buttons)
        {
            item.interactable = !state;
        }
    }
}
