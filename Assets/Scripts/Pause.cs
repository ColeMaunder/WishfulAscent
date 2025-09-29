using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject [] pauseScreen;
    [SerializeField] Sprite [] controllsScreens;
    [SerializeField] AudioClip menueMusic;
    [SerializeField] float musicVolume = 0;
    FPController controller;
    AudioHandler sound;
    void Start()
    {
        pauseScreen[0].SetActive(false);
        controller = GameObject.FindWithTag("Player").GetComponent<FPController>();
        sound = GameObject.FindWithTag("Managers").GetComponent<AudioHandler>();
    }
    public void ActivetePause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (Time.timeScale != 0) {
               Time.timeScale = 0;
                sound.FaidBetweenWorldSound(menueMusic,musicVolume,8f,0);
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
        sound.FaidBetweenWorldSound(GameObject.FindWithTag("Configurer").GetComponent<SceneConfiger>().GetMusic(),musicVolume,10f,0);
        sound.GetComponent<AudioSource>().Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen[0].SetActive(false);
        pauseScreen[1].SetActive(false);
        pauseScreen[2].SetActive(false);
        controller.DisableChricters(false);
    }
    public void Quit(){
        print("Quit");
        Application.Quit();
    }
    public void controllesScrean(bool state){
        pauseScreen[1].SetActive(!state);
        pauseScreen[2].SetActive(state);
        if(state){
            //Image controllsDisplay = pauseScreen[2].GetComponent<Image>();
            switch (controller.GetControlScheme())
            {
                case "Keyboard":
                    //pauseScreen[2].GetComponent<Image>().sou
                    print("Keyboard");
                    break;
                case "Gamepad":
                    print("Gamepad");
                    break;
            }
            
        }
    }
}
