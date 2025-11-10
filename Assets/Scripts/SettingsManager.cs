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
    GameObject windowScreen;
    [SerializeField]
    Button[] buttons;
    private void OnEnable() {
        EventSystem.current.SetSelectedGameObject(transform.GetChild(3).gameObject);
        AllInteractible(false);
        AllActivated(false);
    }
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
        if(!state){
            EventSystem.current.SetSelectedGameObject(transform.GetChild(5).gameObject);
        }
    }
    public void Window(bool state){
        windowScreen.SetActive(state);
        AllInteractible(state);
        if(state){
            EventSystem.current.SetSelectedGameObject(windowScreen.transform.GetChild(1).gameObject);
        }else{
             EventSystem.current.SetSelectedGameObject(transform.GetChild(4).gameObject);
        }
    }
    private void AllInteractible(bool state) {
        foreach (var item in buttons)
        {
            item.gameObject.GetComponent<ButtonSelectHandler>().enabled = !state;
            //item.interactable = !state;
        }
    }
    private void AllActivated(bool state) {
        foreach (var item in buttons){
            if(item.gameObject.GetComponent<SettinsSalectHandler>() != null){
                item.gameObject.GetComponent<SettinsSalectHandler>().SetActivated(state);
            }
        }
    }
    
}
