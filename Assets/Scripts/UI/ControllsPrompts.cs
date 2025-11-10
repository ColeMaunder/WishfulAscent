using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllsPrompts : MonoBehaviour
{
    [SerializeField] Sprite [] controller;
    [SerializeField] Sprite[] keyboard;

    FPController fpController;
    GameObject displayUI;
    Image icon;
    void Awake()
    {
        
        displayUI = transform.GetChild(0).gameObject;
        icon = displayUI.transform.GetChild(0).gameObject.GetComponent<Image>();
        fpController = GameObject.FindWithTag("Player").GetComponent<FPController>();
        TriggerCamra(fpController.GetActiveCharicter().name);
    }

    public void TiggerControll(int id){
        switch (fpController.GetControlScheme())
            {
                case "Keyboard":
                icon.sprite = keyboard[id];
                    break;
                case "Gamepad":
                icon.sprite = controller[id];
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
        if (GetCamra() != (fpController.GetActiveCharicter() + " UI")) {
           gameObject.GetComponent<Canvas>().worldCamera = null; 
        }
    }
}
