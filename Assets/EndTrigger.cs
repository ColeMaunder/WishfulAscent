using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneChanger.ChangeScene.GoToScene("Complete");
    }
}
