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
        if(screens.value == 3){
            dimentions.gameObject.SetActive(true);
            dimentions.value = WindowMode.settings.dimentionID;
        } else{
            dimentions.gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(screens.gameObject);
    }
    
    public void updateWindow(){
        if (screens.value == 3) {
            dimentions.gameObject.SetActive(true);
            WindowMode.settings.dimentionID = dimentions.value;
        } else {
            dimentions.gameObject.SetActive(false);
            WindowMode.settings.activeMode = screens.value;
        }
    }
    public void salectBack(GameObject next){
        EventSystem.current.SetSelectedGameObject(next);
    }
}
