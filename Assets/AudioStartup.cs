using UnityEngine;
using UnityEngine.UI;

public class AudioStartup : MonoBehaviour
{
    [SerializeField]
    Slider[] audioSliders;
    void Awake()
    {
        audioSliders[0].value = PlayerPrefs.GetFloat("Master");
        audioSliders[1].value = PlayerPrefs.GetFloat("SoundEffects");
        audioSliders[2].value = PlayerPrefs.GetFloat("Music");
        audioSliders[3].value = PlayerPrefs.GetFloat("Dialogue");

    }
}
