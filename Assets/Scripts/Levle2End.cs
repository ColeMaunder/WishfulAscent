using UnityEngine;

public class Levle2End : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MagicPowerd>() != null) {
            if(other.gameObject.GetComponent<MagicPowerd>().GetEmpowered()) {
                Saving.activeSave.roomPrgress = 2;
            }
        }
    }
}
