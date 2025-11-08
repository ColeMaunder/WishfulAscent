using UnityEngine;
using UnityEngine.Video;

public class firstAnamtion : MonoBehaviour
{
    VideoPlayer video;
    void Start()
    {
        video = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update(){
        if(video.isPaused){
            gameObject.SetActive(false);
        }
    }
}
