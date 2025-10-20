using TMPro;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    public SaveData save;
    [SerializeField]
    TMP_Text [] textBoxes;

    public void TriggerLaodSave(){
        Saving.saver.LoadSave(save.saveID);
    }
    public void UpdateText() {
        textBoxes[0].text = "Save: " + save.saveID;
        textBoxes[1].text = "Location: " + save.currentScene;
        textBoxes[2].text = "Last Accessed: " + save.whenSaved;
        textBoxes[3].text = "Play Time : " + save.playTime;
    }
    public void BalnkText() {
        foreach (var item in textBoxes) {
            item.text = "";
        }
    }
}
