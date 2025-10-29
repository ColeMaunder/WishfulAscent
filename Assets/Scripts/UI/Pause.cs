
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;
using System.Collections.Generic;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject [] pauseScreen;
    
    [SerializeField] AudioClip menueMusic;
    [SerializeField] float musicVolume = 0;
    FPController controller;
    EventSystem eventSystem;
    AudioSource[] audioSources = { null, null, null };
    List<AudioSource> waitingDialog = new List<AudioSource>();
    
    void Start()
    {
        eventSystem = GameObject.FindAnyObjectByType<EventSystem>(); 
        pauseScreen[0].SetActive(false);
        controller = GameObject.FindWithTag("Player").GetComponent<FPController>();
        for (int i = 0; i < 3; i++){
            audioSources[i] = controller.transform.GetChild(i).GetChild(0).GetComponent<AudioSource>();
        }
    }
    private void OnEnable() {
        
    }
    public void ActivetePause() {
        if (pauseScreen[0]) {
            if (Time.timeScale != 0) {
               Time.timeScale = 0;
                AudioHandler.Audio.FaidBetweenWorldSound(musicVolume,10,0,1,menueMusic);
                AudioHandler.Audio.GetComponent<AudioSource>().Play();
                pauseScreen[0].SetActive(true);
                pauseScreen[1].SetActive(true);
                pauseScreen[2].SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                controller.DisableChricters(true);
                EventSystem.current.SetSelectedGameObject(pauseScreen[1].transform.GetChild(1).gameObject);
                foreach (var item in audioSources) {
                    if (item.isPlaying) {
                        waitingDialog.Add(item);
                        item.Pause();
                    }
                }
            } else {
                DeactivatePause();
            }
        }
    }
    public void DeactivatePause(){
        Time.timeScale = 1;
        AudioHandler.Audio.FaidBetweenWorldSound(musicVolume,10,1,0);
        AudioHandler.Audio.GetComponent<AudioSource>().Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen[0].SetActive(false);
        pauseScreen[1].SetActive(false);
        pauseScreen[2].SetActive(false);
        controller.DisableChricters(false);
        foreach (var item in waitingDialog) {
            item.Play();
        }
        waitingDialog.Clear();
    }
    public void Quit(){
        SceneChanger.ChangeScene.GoToScene("StartScreen");
    }
    public void SettingsScrean(bool state){
        pauseScreen[1].SetActive(!state);
        pauseScreen[2].SetActive(state);
        if(state){
            eventSystem.SetSelectedGameObject(pauseScreen[2].transform.GetChild(3).gameObject);
        }else{
            eventSystem.SetSelectedGameObject(pauseScreen[1].transform.GetChild(2).gameObject);
        }
    }
}
