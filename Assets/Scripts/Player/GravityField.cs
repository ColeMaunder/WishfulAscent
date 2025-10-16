using Unity.VisualScripting;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    Transform gravOrb;
    [SerializeField]
    private float force = 30;
    void Start()
    {
        gravOrb = transform.parent;
    }
    void OnTriggerStay(Collider collision) {
        if(collision.gameObject.GetComponent<TimeForce>()  != null){
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            Vector3 direction = Vector3.Normalize(collision.transform.position - gravOrb.position);
            //tf.Gravity(false);
            tf.AddForce(direction * - force,ForceMode.Acceleration);
        }
    } 
    void OnTriggerExit(Collider collision) {
        if(collision.gameObject.tag == "Object"){
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            //tf.Gravity(true);
            tf.Sleep();
        }
    } 
}
