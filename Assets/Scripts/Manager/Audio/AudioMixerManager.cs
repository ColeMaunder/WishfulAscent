using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    void Start()
    {
        setMaster(PlayerPrefs.GetFloat("Master"));
        setSound(PlayerPrefs.GetFloat("SoundEffects"));
        setMusic(PlayerPrefs.GetFloat("Music"));
        setDialogue(PlayerPrefs.GetFloat("Dialogue"));
    }
    [SerializeField]
    private AudioMixer audioMixer;
    public void setMaster(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Master", volume);
    }
    public void setSound(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("SoundEffects", volume);
    }
    public void setMusic(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Music", volume);
    }
    public void setDialogue(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Dialogue", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("Dialogue", volume);
    }
}

