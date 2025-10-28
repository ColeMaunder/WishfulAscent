using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllsPrompts : MonoBehaviour
{
    [SerializeField] Sprite [] controller;
    [SerializeField] Sprite[] keyboard;
    [SerializeField] string [] nameControll;

    FPController fpController;
    GameObject displayUI;
    Image icon;
    TMP_Text discription;
    void Awake()
    {
        
        displayUI = transform.GetChild(0).gameObject;
        icon = displayUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        discription = displayUI.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        fpController = GameObject.FindWithTag("Player").GetComponent<FPController>();
        TriggerCamra(fpController.GetActiveCharicter().name);
    }

    public void TiggerControll(int id){
        switch (fpController.GetControlScheme())
            {
                case "Keyboard":
                icon.sprite = keyboard[id];
                discription.text = nameControll[id];
                    break;
                case "Gamepad":
                icon.sprite = controller[id];
                discription.text = nameControll[id];
                    break;
            }
    }
    public void activateControllUI(bool state){
        displayUI.SetActive(state);
    }
    public void TriggerCamra(string name){
        Camera commonUICamra = GameObject.Find(name +" UI").gameObject.GetComponent<Camera>();
        gameObject.GetComponent<Canvas>().worldCamera = commonUICamra;
    }
    public string GetCamra(){
        try{
            return gameObject.GetComponent<Canvas>().worldCamera.name;
        }catch(NullReferenceException){
            return "";
        }
        
    }
    public void wipe(){
        icon.sprite = null;
        discription.text = "";
        if (GetCamra() != (fpController.GetActiveCharicter() + " UI")) {
           gameObject.GetComponent<Canvas>().worldCamera = null; 
        }
    }
}
