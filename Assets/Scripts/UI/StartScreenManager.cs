using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.EventSystems;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField]
    string nextSceen;
    [SerializeField]
    GameObject titleScreen;
    [SerializeField]
    GameObject startScreen;
    [SerializeField]
    GameObject settingsScreen;
    [SerializeField]
    GameObject saveList;
    //[SerializeField]
    //GameObject videoScreen;
    //VideoPlayer videoPlayer;
    [SerializeField]
    float fadeDuration = 2.0f;
    //VideoClip[] videos;

    
    public void NewGame(){
        Saving.saver.newSaveFile();
        SceneChanger.ChangeScene.GoToScene(nextSceen);
    }
    public void LoadSave(bool state){
        saveList.SetActive(state);
    }
    public void ContinueGame(){
        Saving.saver.LoadSave();
    }
    public void swapScreeens(bool toStart){
        StartCoroutine(FadeToStart(toStart));
    }
    IEnumerator FadeToStart(bool toStart)
    {
        CanvasGroup from;
        CanvasGroup to;
        float timer = 0f;
        if (toStart) {
            to = startScreen.GetComponent<CanvasGroup>();
            from = titleScreen.GetComponent<CanvasGroup>();
        } else {
            to = titleScreen.GetComponent<CanvasGroup>();
            from = startScreen.GetComponent<CanvasGroup>();
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(titleScreen.transform.GetChild(2).gameObject);
        }
        to.interactable = false;
        from.interactable = false;
        to.alpha = 0;
        to.gameObject.SetActive(true);
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;

            from.alpha = Mathf.Lerp(1f, 0f, progress);
            to.alpha = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }
        from.alpha = 0;
        from.gameObject.SetActive(false);
        to.alpha = 1;
        to.interactable = true;
        from.interactable = true;
    }
   
    public void Settings(){
        settingsScreen.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(settingsScreen.transform.GetChild(2).gameObject);
        titleScreen.transform.GetChild(0).gameObject.GetComponent<VideoPlayer>().Pause();
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
     /*void Start(){
        videoPlayer = videoScreen.GetComponent<VideoPlayer>();
    }*/
    
    /*public void StartScreen() {
        titleScreen.SetActive(false);
        videoPlayer.clip = videos[1];
        videoPlayer.Play();
        Invoke("DiplayStart", (float) videoPlayer.clip.length);
    }
    private void DiplayStart()
    {
        videoPlayer.clip = videos[2];
        videoPlayer.Play();
        startScreen.SetActive(true);
    }
    public void TitleScreen() {
        startScreen.SetActive(false);
        videoPlayer.clip = videos[1];
        videoPlayer.playbackSpeed = -1f;
        videoPlayer.time = videoPlayer.clip.length;
        videoPlayer.Play();
        Invoke("DiplayTitle", (float) videoPlayer.clip.length);
    }
    private void DiplayTitle(){
        videoPlayer.playbackSpeed = 3f;
        videoPlayer.time = 0;
        videoPlayer.clip = videos[0];
        videoPlayer.Play();
        titleScreen.SetActive(true);
    }*/
}
