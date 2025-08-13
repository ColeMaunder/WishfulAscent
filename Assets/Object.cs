using UnityEngine;

public class Object : MonoBehaviour
{
    public bool holding = false;
    
    Rigidbody rb;

    void Start()
    {
        rb = transform.gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(holding){
           float distance = Vector3.Distance(transform.localPosition, Vector3.zero);
            if (distance > 1){
                SetHoolding(false);
                transform.SetParent(null);          
            }else if (distance > 0.5){
                rb.AddForce(20*(Vector3.zero - transform.localPosition));
            }else{
                rb.angularVelocity = Vector3.zero;
            } 
        }
    }
    public void SetHoolding(bool state){
        if(state != holding){
            rb.useGravity = !state;
        }
        holding = state;
    }
    void OnCollisionEnter(Collision collision)
    {
        print("Hit");
        //if (holding){
        rb.isKinematic = false;
            //rb.AddForce(collision.relativeVelocity);
        //}
        
    } 
}
