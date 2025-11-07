using UnityEngine;

public class StopRail : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay(Collider other) {
        Transform  wheel = other.transform;
        if (wheel.name == "Cart Wheel") {
            wheel.GetComponent<Rigidbody>().Sleep();
        }
    }
}
