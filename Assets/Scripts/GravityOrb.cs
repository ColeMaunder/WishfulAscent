using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    public bool active = false;
    
    Rigidbody rb;

    void Start() {
        rb = transform.gameObject.GetComponent<Rigidbody>();
    }
    public void ToggleActive(bool state) {
        active = state;
    }
    public bool isActive()
    {
        return active;
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag != "Object"){
            rb.Sleep();
        }
    } 
}
