using UnityEngine;

public class EnableSwap : MonoBehaviour
{
    
    [SerializeField] int progressRequired;
    private void OnTriggerStay(Collider other) {
        Transform enterObject = other.transform;
        if (Saving.activeSave.roomPrgress == progressRequired && enterObject.GetComponent<CharacterController>() != null) {
            enterObject.parent.GetComponent<FPController>().swapEnabled = true;
        }
    }
}
