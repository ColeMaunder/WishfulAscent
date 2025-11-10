using UnityEngine;

public class WindowMode : MonoBehaviour
{
    public int activeMode;
    public int dimentionID;
    [SerializeField] int[] dimentionMods;
    public static WindowMode settings;
    public void instantiate()
    {
        settings = this;
        if (PlayerPrefs.GetInt("WindowMode") == 2)
        {
            setScreenDimention(PlayerPrefs.GetInt("WindowDimentionID"));
        } else {
            setScreen(PlayerPrefs.GetInt("WindowMode"));
        }
    }
    public void setScreen(int WindowMode){
        switch (WindowMode)
        {
            case 0:
                SetBorderlessWindow();
                break;
            case 1:
                SetExclusiveFullscreen();
                break;
        }
    }
    public void setScreenDimention(int WindowDimentionID ){
        SetWindowedMode(WindowDimentionID);
            
    }
    public void SetBorderlessWindow() {
        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
        PlayerPrefs.SetInt("WindowMode", 0);
        PlayerPrefs.Save();
        activeMode = 0;
    }

    public void SetExclusiveFullscreen() {
        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;
        Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen);
        PlayerPrefs.SetInt("WindowMode", 1);
        PlayerPrefs.Save();
        activeMode = 1;
    }

    public void SetWindowedMode(int id) {
        Screen.SetResolution(dimentionMods[id] * 16, dimentionMods[id] * 9, FullScreenMode.Windowed);
        PlayerPrefs.SetInt("WindowMode", 2);
        PlayerPrefs.SetInt("WindowDimentionID", id);
        PlayerPrefs.Save();
        dimentionID = id;
        activeMode = 2;
    }
}
