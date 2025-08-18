using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    FPController controller;
    void Start()
    {
        pauseScreen.SetActive(false);
        controller = GameObject.FindWithTag("Player").GetComponent<FPController>();
    }
    public void ActivetePause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (Time.timeScale != 0) {
               Time.timeScale = 0;
                pauseScreen.SetActive(true);
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseScreen.SetActive(false);
        controller.DisableChricters(false);
    }
    public void Quit(){
        print("Quit");
        Application.Quit();
    }
}
