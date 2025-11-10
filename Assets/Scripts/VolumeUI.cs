using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] Slider master;
    [SerializeField] Slider sound;
    [SerializeField] Slider music;
    [SerializeField] Slider dailog;

    void OnEnable() {
        master.value = PlayerPrefs.GetFloat("Master");
        sound.value = PlayerPrefs.GetFloat("SoundEffects");
        music.value = PlayerPrefs.GetFloat("Music");
        dailog.value = PlayerPrefs.GetFloat("Dialogue");
    }

    public void setMaster(float volume){
        AudioMixerManager.settings.setMaster(volume);
    }
    public void setSound(float volume){
        AudioMixerManager.settings.setSound(volume);
    }
    public void setMusic(float volume) {
        AudioMixerManager.settings.setMusic(volume);
    }
    public void setDialogue(float volume) {
        AudioMixerManager.settings.setDialogue(volume);
    }
}
