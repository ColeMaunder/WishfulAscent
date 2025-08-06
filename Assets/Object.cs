using UnityEngine;

public class Object : MonoBehaviour
{
    public bool holding = false;
    
    Rigidbody rb;

    void Start()
    {
        rb = transform.gameObject.GetComponent<Rigidbody>();
    }
    //public void SetPosition(Transform )
    //{

    //}
    public void SetHoolding(bool state){
        holding = state;
    }
    void OnCollisionStay(Collision collision)
    {
        if (holding){
            rb.AddForce(collision.relativeVelocity);
        }
        
    } 
}
