
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Image = UnityEngine.UI.Image;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject [] pauseScreen;
    [SerializeField] Sprite [] controllsScreens;
    [SerializeField] AudioClip menueMusic;
    [SerializeField] float musicVolume = 0;
    FPController controller;
    AudioHandler sound;
    SceneChanger changer;
    EventSystem eventSystem;
    void Start()
    {
        eventSystem = GameObject.FindAnyObjectByType<EventSystem>(); 
        pauseScreen[0].SetActive(false);
        controller = GameObject.FindWithTag("Player").GetComponent<FPController>();
        sound = GameObject.FindWithTag("Managers").GetComponent<AudioHandler>();
        changer = GameObject.FindWithTag("Managers").GetComponent<SceneChanger>();
    }
    
    public void ActivetePause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (Time.timeScale != 0) {
               Time.timeScale = 0;
                sound.FaidBetweenWorldSound(musicVolume,10,0,1,menueMusic);
                sound.GetComponent<AudioSource>().Play();
                pauseScreen[0].SetActive(true);
                pauseScreen[1].SetActive(true);
                pauseScreen[2].SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                controller.DisableChricters(true); 
            } else {
                DeactivatePause();
            }
        }
    }
    public void DeactivatePause(){
        Time.timeScale = 1;
        sound.FaidBetweenWorldSound(musicVolume,10,1,0);
        sound.GetComponent<AudioSource>().Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen[0].SetActive(false);
        pauseScreen[1].SetActive(false);
        pauseScreen[2].SetActive(false);
        controller.DisableChricters(false);
    }
    public void Quit(){
        changer.GoToScene("StartScreen");
    }
    public void controllesScrean(bool state){
        pauseScreen[1].SetActive(!state);
        pauseScreen[2].SetActive(state);
        if(state){
            eventSystem.SetSelectedGameObject(pauseScreen[2].transform.GetChild(0).gameObject);
            switch (controller.GetControlScheme())
            {
                case "Keyboard":
                    pauseScreen[2].GetComponent<Image>().sprite = controllsScreens[0];
                    print("Keyboard");
                    break;
                case "Gamepad":
                    print("Gamepad");
                    pauseScreen[2].GetComponent<Image>().sprite = controllsScreens[1];
                    break;
            }
            
        }else{
            eventSystem.SetSelectedGameObject(pauseScreen[1].transform.GetChild(2).gameObject);
        }
    }
}
