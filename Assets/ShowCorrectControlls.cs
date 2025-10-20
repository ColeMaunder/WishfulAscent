using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using UnityEngine.EventSystems;
using System;
public class ShowCorrectControlls : MonoBehaviour
{
    [SerializeField]
    Sprite[] controllsScreens;
    [SerializeField]
    GameObject[] buttonOptions;
    [SerializeField]
    Sprite [] buttonIcons;
    void OnEnable() {
        try {
            FPController controller = GameObject.FindWithTag("Player").GetComponent<FPController>();
            SetIcon(controller.GetControlScheme());
            setAllButtons(false);
        } catch(NullReferenceException) {
            setAllButtons(true); 
        }
    }
    public void SetSalect() {
        if (!buttonOptions[0].activeSelf) {
            EventSystem.current.SetSelectedGameObject(transform.GetChild(1).gameObject);
        } else {
            EventSystem.current.SetSelectedGameObject(transform.GetChild(2).gameObject);
        }
    }
    private void setAllButtons(bool state)
    {
        foreach (var item in buttonOptions) {
            item.SetActive(state);
            item.GetComponent<Button>().interactable = state;
        }
    }
    
    public void SetIcon( string name){
        Image dispaly = gameObject.GetComponent<Image>();
        switch (name) {
            case "Keyboard":
                dispaly.sprite = controllsScreens[0];
                print("Keyboard");
                break;
            case "Gamepad":
                print("Gamepad");
                dispaly.sprite = controllsScreens[1];
                break;
        }
    }
    public void SetAllButtonIcons(int type)
    {
        for (int i = 0; i < buttonOptions.Length; i++)
        {
            buttonOptions[i].gameObject.GetComponent<Image>().sprite = buttonIcons[i +type];
        }
    }
}
