using UnityEngine;

public class SceneConfiger : MonoBehaviour
{
    [SerializeField]
     float timeScale = 1f;
     [SerializeField]
     float musicVolume = 1f;
    // [SerializeField]
    // int sceneCahngeID = 0;
    [SerializeField]
    private AudioClip seaneMusic;
    void Awake()
    {
        Time.timeScale = timeScale;
        DoNotDistroy[] allDoNotDistroy = FindObjectsByType<DoNotDistroy>(FindObjectsSortMode.None);
        foreach(DoNotDistroy i in allDoNotDistroy){
            i.OnSceneLoaded();
        }
        AudioHandler sound = GameObject.FindWithTag("Managers").GetComponent<AudioHandler>();
        //GameObject.FindWithTag("SceneChainger").GetComponent<SceneChanger>().SetCahingID(sceneCahngeID);
        sound.FaidBetweenWorldSound(seaneMusic,musicVolume,8f,0);
        sound.GetComponent<AudioSource>().Play();
        
    }
    
}
