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
    GameObject [] buttonOptions;
    void OnEnable() {
        try {
            FPController controller = GameObject.FindWithTag("Player").GetComponent<FPController>();
            SetIcon(controller.GetControlScheme());
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
            setAllButtons(false);
        } catch(NullReferenceException) {
            setAllButtons(true);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(1).gameObject);
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
}
