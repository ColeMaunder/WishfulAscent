using UnityEngine;

public class EndTrigger : MonoBehaviour

{
    [SerializeField] string nextScene;
    void OnTriggerEnter(Collider other)
    {
        NextCutsene nextC = FindFirstObjectByType<NextCutsene>();
        nextC.nextCutsene = 1;
        nextC.nextScene = nextScene;
        SceneChanger.ChangeScene.GoToScene("Cutscene");

    }
}
