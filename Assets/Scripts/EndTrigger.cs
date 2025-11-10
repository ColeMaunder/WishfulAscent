using UnityEngine;

public class EndTrigger : MonoBehaviour

{
    [SerializeField] string nextScene;
    [SerializeField] int nextCutsene = 1;
    void OnTriggerEnter(Collider other)
    {
        NextCutsene nextC = FindFirstObjectByType<NextCutsene>();
        nextC.nextCutsene = nextCutsene;
        nextC.nextScene = nextScene;
        SceneChanger.ChangeScene.GoToScene("Cutscene");

    }
}
