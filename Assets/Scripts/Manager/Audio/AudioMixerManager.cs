using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    public void setMaster(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20f);
    }
    public void setSound(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("SoundEffects", Mathf.Log10(volume) * 20f);
    }
    public void setMusic(float volume){// between 0.0001 and 1
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
    }
}

