using UnityEngine;

public class PowedWatcher : MonoBehaviour
{
    [SerializeField] MagicPowerd cart;
    void Update() {
        if (cart.GetEmpowered()){
            Saving.activeSave.roomPrgress = 1;
        }
    }
}
