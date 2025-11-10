using UnityEngine;

public class TriggerMagic : MonoBehaviour
{
    [SerializeField] MagicDipencer dropper;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cart Boddy") {
            dropper.SetDropping(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Cart Boddy") {
            dropper.SetDropping(false);
        }
    }
}
