using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowRail : MonoBehaviour
{
    Transform rail;
    void Update() {
        if(rail != null) {
        //transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
        //transform.localScale = new Vector3(0.2f / rail.localScale.x, 0.1f / rail.localScale.y, 0.1f / rail.localScale.z);
        /*while (Quaternion.Angle(transform.localRotation, Quaternion.identity)>1) {
                //Quaternion.Angle(transform.localRotation, Quaternion.identity);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 10);
                //transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0), Space.Self);
        }*/
        //transform.localRotation = new Quaternion(transform.localRotation.x,0,0,1); 
        }
    }
    void OnTriggerEnter(Collider other) {
        Transform inRail = other.transform;
        if (inRail.tag == "Rail") {
            rail = inRail;
            //transform.SetParent(rail);
            gameObject.GetComponent<ConfigurableJoint>().axis = inRail.right;
            gameObject.GetComponent<ConfigurableJoint>().secondaryAxis = inRail.up;
            transform.position = new Vector3(transform.position.x, inRail.position.y, inRail.position.z);
            //transform.localScale = new Vector3(0.2f / rail.localScale.x, 0.1f / rail.localScale.y, 0.1f / rail.localScale.z);
            /*while (Quaternion.Angle(transform.rotation, inRail.rotation)>1) {
                //Quaternion.Angle(transform.localRotation, Quaternion.identity);
                transform.rotation = Quaternion.Slerp(transform.rotation, inRail.rotation, Time.deltaTime * 10);
                //transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0), Space.Self);
            }*/
            //transform.rotation = new Quaternion(transform.rotation.x, inRail.rotation.y, inRail.rotation.z, 1);
            
        }
    }
}
