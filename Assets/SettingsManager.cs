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
            EventSystem.current.SetSelectedGameObject(transform.GetChild(4).gameObject);
        }
    }
    public void Window(bool state){
        windowScreen.SetActive(state);
        AllInteractible(state);
        if(!state){
            EventSystem.current.SetSelectedGameObject(transform.GetChild(5).gameObject);
        }
    }
    private void AllInteractible(bool state) {
        foreach (var item in buttons)
        {
            item.interactable = !state;
        }
    }
    public void setMaster(float volume){
        AudioMixerManager.settings.setMaster(volume);
    }
    public void setSound(float volume){
        AudioMixerManager.settings.setSound(volume);
    }
    public void setMusic(float volume) {
        AudioMixerManager.settings.setMusic(volume);
    }
    public void setDialogue(float volume) {
        AudioMixerManager.settings.setDialogue(volume);
    }
}
