using UnityEngine;

public class LoadSave : MonoBehaviour
{
    public SaveData save;

    public void TriggerLaodSave(){
        Saving.saver.LoadSave(save.saveID);
    }
}
