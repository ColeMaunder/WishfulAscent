using UnityEngine;

public class TriggerRailTurn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        GameObject cart = other.gameObject;
        if(cart.GetComponent<AutoRail>() != null) {
            cart.GetComponent<AutoRail>().turn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        GameObject cart = other.gameObject;
        if(cart.GetComponent<AutoRail>() != null) {
            cart.GetComponent<AutoRail>().turn = false;
        }
    }
}
