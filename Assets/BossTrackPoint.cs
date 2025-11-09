using UnityEngine;

public class BossTrackPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Boss>() != null){
            other.GetComponent<Boss>().hitPoint = true;
        }
    }
}
