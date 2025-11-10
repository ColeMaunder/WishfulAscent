using UnityEngine;

public class EndTrigger : MonoBehaviour

{
    [SerializeField] string nextScene;
    void OnTriggerEnter(Collider other)
    {
        SceneChanger.ChangeScene.GoToScene(nextScene);
    }
}
