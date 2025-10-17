using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllsPrompts : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Sprite [] controller;
    [SerializeField] Sprite[] keyboard;
    //[SerializeField] string [] controllerName;
    //[SerializeField] string [] keyboardName;
    FPController fpController;
    GameObject displayUI;
    Image icon;
    TMP_Text discription;
    void Awake()
    {
        Camera commonUICamra = GameObject.Find("Common UI").gameObject.GetComponent<Camera>();
        gameObject.GetComponent<Canvas>().worldCamera = commonUICamra;
        displayUI = transform.GetChild(0).gameObject;
        icon = displayUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        discription = displayUI.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        fpController = GameObject.FindWithTag("Player").GetComponent<FPController>();
    }

    public void TiggerControll(int id){
        activateControllUI(true);
        switch (fpController.GetControlScheme())
            {
                case "Keyboard":
                icon.sprite = keyboard[id];
                //discription.text = keyboardName[id];
                    break;
                case "Gamepad":
                icon.sprite = controller[id];
                //discription.text = controllerName[id];
                    break;
            }
    }
    public void activateControllUI(bool state){
        displayUI.SetActive(state);
    }
}
