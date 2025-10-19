using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ContinueCheck : MonoBehaviour
{
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    GameObject newGame;
    [SerializeField]
    float diabledOpcity = 0.5f;
    
    
    void OnEnable() {
        bool existingSaves = Saving.saver.AreExistingSaves();
        EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        foreach (GameObject button in buttons) {
            TMP_Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
            button.GetComponent<Button>().interactable = existingSaves;
            Color currentCouler = buttonText.color;
            if (existingSaves) {
                currentCouler.a = 1f;
                eventSystem.SetSelectedGameObject(buttons[0]);
            } else {
                currentCouler.a = diabledOpcity;
                eventSystem.SetSelectedGameObject(newGame);
            }
            buttonText.color = currentCouler;
        }

    }
}
