using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    public bool active = false;
    
    Rigidbody rb;

    void Start() {
        rb = transform.gameObject.GetComponent<Rigidbody>();
    }
    void Update() {
        if (active){
            
        }
    }
    public void ToggleActive(bool state) {
        active = state;
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag != "Object"){
            rb.Sleep();
        }
    } 
}
