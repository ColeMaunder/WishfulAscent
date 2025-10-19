using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SaveList : MonoBehaviour
{
    [SerializeField]
    Transform[] buttons;
    [SerializeField]
    Slider slider;
    List<SaveData> fileDirectory;
    void OnEnable() {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(buttons[0].gameObject);
        fileDirectory = Saving.saver.saveList;
        if (fileDirectory.Count > 3) {
            slider.interactable = true;
            slider.maxValue = Mathf.Ceil(fileDirectory.Count / 3);
        } else {
            slider.interactable = false;
        }
        setOptions();
    }
    
    public void setOptions() {
        for (int i = 0; i < 3; i++) {
            try {
                buttons[i].gameObject.SetActive(true);
                buttons[i].gameObject.GetComponent<Button>().interactable = true;
                buttons[i].gameObject.GetComponent<LoadSave>().save = fileDirectory[i + (int)(slider.value * 3)];
                buttons[i].GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Save: " +fileDirectory[i + (int) (slider.value * 3)].saveID;
            } catch (ArgumentOutOfRangeException) {
                buttons[i].gameObject.GetComponent<Button>().interactable = false;
                buttons[i].GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
                //buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
