using UnityEngine;

public class CutseneManager : MonoBehaviour
{
    public Sprite[] Scene1;
    public SpriteRenderer diplay;
    public int firstFrame = 0;
    private int currentFrame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentFrame = firstFrame;
        diplay.sprite = Scene1[currentFrame];
    }
    public void nextFrame(){
        currentFrame++;
        if (Scene1.Length > currentFrame) {
            currentFrame = 0;
        }
        diplay.sprite = Scene1[currentFrame];
    }
}
