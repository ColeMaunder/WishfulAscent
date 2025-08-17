using UnityEngine;

public class GravityOrb : MonoBehaviour
{
    public bool holding = false;
    
    Rigidbody rb;

    void Start() {
        rb = transform.gameObject.GetComponent<Rigidbody>();
    }
    void Update() {
    }
    public void SetHoolding(bool state) {
        if(state != holding){
            rb.useGravity = !state;
        }
        holding = state;
    }
    void OnCollisionEnter(Collision collision) {
        rb.Sleep();
    } 
}
