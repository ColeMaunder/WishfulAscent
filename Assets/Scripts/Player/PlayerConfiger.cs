using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConfigurer : MonoBehaviour
{
    [SerializeField]
    private Transform[] startPositions;
    [SerializeField]
    private string wakeDialog = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        Transform playerPerent = GameObject.FindWithTag("Player").transform;
        /*for(int i = 0; i <= 1;i++){
            players[i] = playerPerent.GetChild(1);
        }*/
        for(int i = 0; i <= 1;i++){
           playerPerent.GetChild(i).position = startPositions[i].position;
        }
        Saving.saver.PerformSave(scene);
        if(wakeDialog != "") {
            DialogManiger.Dialog.RunSequence(scene, wakeDialog,1);
        }
    }
}
