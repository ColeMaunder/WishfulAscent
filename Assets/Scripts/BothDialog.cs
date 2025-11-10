using UnityEngine;
using UnityEngine.SceneManagement;
public class BothDialog : MonoBehaviour
{
    [SerializeField] GameObject[] watchers;
    [SerializeField] string sequence;
    [SerializeField] int roomProgress = 1;
    bool diplay = true;
    string scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        //scene = "LevelOne";
    }
    void Update()
    {
        check();
    }
    public void check() {
        diplay = true;
        foreach (var item in watchers) {
            if (item.activeSelf) {
                diplay = false;
            }
        }
        if(diplay){
            if (roomProgress == 0)
            {
                DialogManiger.Dialog.RunSequence(scene, sequence);
            }
            else
            {
                DialogManiger.Dialog.RunSequence(scene, sequence, roomProgress);
            }
            gameObject.SetActive(false);
        }
    }
    
}
