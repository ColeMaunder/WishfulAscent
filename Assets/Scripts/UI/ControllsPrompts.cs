using UnityEngine;

public class ControllsPrompts : MonoBehaviour
{
    [SerializeField] GameObject [] pauseScreen;
    [SerializeField] Sprite [] controller;
    [SerializeField] Sprite [] keyboard;
    void Awake()
    {
        Camera commonUICamra = GameObject.FindWithTag("UICamras").transform.GetChild(2).gameObject.GetComponent<Camera>();
        gameObject.GetComponent<Canvas>().worldCamera = commonUICamra;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
