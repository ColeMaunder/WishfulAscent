using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroCutseneManger : MonoBehaviour
{
    DialogManiger dialogManager;
    [SerializeField]
    Sprite[] shots;
    [SerializeField]
    Image shotDisplay;
    [SerializeField]
    TMP_Text lineDisplay;
    [SerializeField]
    GameObject nextButton;
    [SerializeField]
    float typeSpeed;
    Coroutine scene;
    int shotID = 0;
    [SerializeField]
    string nextScene;

    void Start()
    {
        dialogManager = GameObject.FindWithTag("Managers").GetComponent<DialogManiger>();
        scene = StartCoroutine(runCutsene());
    }

    public IEnumerator runCutsene() {
        DialogueLine dialog = dialogManager.GetDialogue(SceneManager.GetActiveScene().name, "Stella", shotID);
        lineDisplay.text = "";
        foreach (char letter in dialog.text.ToCharArray()) {
            lineDisplay.text += letter;
            yield return new WaitForSecondsRealtime(typeSpeed);
        }
        shotID++;
        nextButton.SetActive(true);
    }
    public void nextshot(){
        if (shotID <= shots.Length) {
            nextButton.SetActive(true);
            scene = StartCoroutine(runCutsene());
        }else{
            GameObject.FindWithTag("Managers").GetComponent<SceneChanger>().GoToScene(nextScene);
        }
    }
}