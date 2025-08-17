using Unity.VisualScripting;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    Transform gravOrb;
    void Start()
    {
        gravOrb = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider collision) {
        print("Hit");
        if(collision.gameObject.tag == "Object"){
            print("found");
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direction = Vector3.Normalize(collision.transform.position - gravOrb.position);
            rb.AddForce(direction * -15,ForceMode.Acceleration);
        }
    } 
    void OnTriggerExit(Collider collision) {
        if(collision.gameObject.tag == "Object"){
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.Sleep();
        }
    } 
}
