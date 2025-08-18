using Unity.VisualScripting;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    Transform gravOrb;
    void Start()
    {
        gravOrb = transform.parent;
    }
    void OnTriggerStay(Collider collision) {
        if(collision.gameObject.tag == "Object"){
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            Vector3 direction = Vector3.Normalize(collision.transform.position - gravOrb.position);
            tf.AddForce(direction * -15,ForceMode.Acceleration);
        }
    } 
    void OnTriggerExit(Collider collision) {
        if(collision.gameObject.tag == "Object"){
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            tf.Sleep();
        }
    } 
}
