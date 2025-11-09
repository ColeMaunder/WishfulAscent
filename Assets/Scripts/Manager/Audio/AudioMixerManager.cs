using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager settings;
    [SerializeField]
    private AudioMixer audioMixer;
    public void instantiate() {
        settings = this;
        setMaster(PlayerPrefs.GetFloat("Master"));
        setSound(PlayerPrefs.GetFloat("SoundEffects"));
        setMusic(PlayerPrefs.GetFloat("Music"));
        setDialogue(PlayerPrefs.GetFloat("Dialogue"));
    }
    public void setMaster(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Master", volume);
        PlayerPrefs.Save();
    }
    public void setSound(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SoundEffects", volume);
        PlayerPrefs.Save();
    }
    public void setMusic(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }
    public void setDialogue(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Dialogue", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Dialogue", volume);
        PlayerPrefs.Save();
    }
}

