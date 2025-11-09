using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WindowModeUI : MonoBehaviour
{
    [SerializeField] TMP_Dropdown screens;
    [SerializeField] TMP_Dropdown dimentions;
    void OnEnable() {
        screens.value = WindowMode.settings.activeMode;
        //EventSystem.current.SetSelectedGameObject(screens.gameObject);
        if(screens.value == 2){
            dimentions.gameObject.SetActive(true);
            dimentions.value = WindowMode.settings.dimentionID;
        } else{
            dimentions.gameObject.SetActive(false);
        }
        //EventSystem.current.SetSelectedGameObject(screens.gameObject);
    }
    
    public void updateWindow(){
        if (screens.value == 2) {
            dimentions.gameObject.SetActive(true);
            WindowMode.settings.setScreenDimention(dimentions.value);
        } else {
            dimentions.gameObject.SetActive(false);
            WindowMode.settings.setScreen(screens.value);
        }
    }
    public void salectBack(GameObject next){
        EventSystem.current.SetSelectedGameObject(next);
    }
}
