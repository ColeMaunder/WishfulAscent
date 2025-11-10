using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class IntroCutseneManger : MonoBehaviour
{
    [SerializeField]
    Sprite[] cutscenes;
    [SerializeField]
    Image shotDisplay;
    [SerializeField]
    TMP_Text lineDisplay;
    [SerializeField]
    GameObject nextButton;
    Coroutine scene;
    int shotID = 0;
    [SerializeField]
    string nextScene;
    string sequence = "Cutscene";

    void Start()
    {
        lineDisplay.text = "";
        scene = StartCoroutine(runCutsene());
        nextButton.GetComponent<Button>().interactable = false;
        nextButton.SetActive(false);
        NextCutsene nextC = FindFirstObjectByType<NextCutsene>();
        int cutseneNum = nextC.nextCutsene;
        nextScene = nextC.nextScene;
        shotDisplay.sprite = cutscenes[cutseneNum-1];
        sequence = sequence + " " + cutseneNum;
    }

    public IEnumerator runCutsene() {
        yield return new WaitForSecondsRealtime(0.5f);
        DialogueLine dialog = DialogManiger.Dialog.GetDialogue(SceneManager.GetActiveScene().name, sequence, shotID);
        AudioSource speeker = transform.GetComponent<AudioSource>();
        speeker.clip = dialog.voiceOverAudio;
        speeker.Play();
        float speed = dialog.voiceOverAudio.length / dialog.text.Length;
        foreach (char letter in dialog.text.ToCharArray()) {
            lineDisplay.text += letter;
            yield return new WaitForSecondsRealtime(speed);
        }
        /*while (speeker.isPlaying){
            yield return new WaitForSeconds(0.5f);
        }*/
        shotID++;
        nextButton.SetActive(true);
        nextButton.GetComponent<Button>().interactable = true;
        EventSystem.current.SetSelectedGameObject(nextButton);
    }
    public void nextshot(){
        if (shotID < 4) {
            nextButton.SetActive(false);
            nextButton.GetComponent<Button>().interactable = true;
            EventSystem.current.SetSelectedGameObject(nextButton);
            lineDisplay.text = "";

            
            scene = StartCoroutine(runCutsene());
        }else{
            SceneChanger.ChangeScene.GoToScene(nextScene);
        }
    }
    public void Skip(){
        SceneChanger.ChangeScene.GoToScene(nextScene);
    }
}