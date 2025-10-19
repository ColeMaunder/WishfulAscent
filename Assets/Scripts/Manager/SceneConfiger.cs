using UnityEngine;
using UnityEngine.SceneManagement;

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
        //GameObject.FindWithTag("SceneChainger").GetComponent<SceneChanger>().SetCahingID(sceneCahngeID);
        AudioHandler.Audio.FaidBetweenWorldSound(musicVolume,8f,0,seaneMusic);
        AudioHandler.Audio.FaideOutWorldSound(8f, 1);
    }
    
    public AudioClip GetMusic(){
        return seaneMusic;
    }
    
}
