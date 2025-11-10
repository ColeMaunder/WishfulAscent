using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    int last = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Saving.activeSave.roomPrgress != last)
        {
            switch (Saving.activeSave.roomPrgress)
            {
                case 1:
                    last = 1;
                    break;
                case 2:
                    DialogManiger.Dialog.RunSequence("Boss", "Hit 1");
                    last = 2;
                    break;
                case 3:
                    DialogManiger.Dialog.RunSequence("Boss", "Hit 2");
                    last = 3;
                    break;
                case 4:
                    last = 4;
                    NextCutsene nextC = FindFirstObjectByType<NextCutsene>();
                    nextC.nextCutsene = 5;
                    nextC.nextScene = "Ending";
                    SceneChanger.ChangeScene.GoToScene("Cutscene");
                    break;
            }
        }
    }
}
