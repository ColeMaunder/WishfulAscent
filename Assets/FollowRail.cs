using Unity.Mathematics;
using UnityEngine;

public class FollowRail : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        Transform rail = other.transform;
        if (rail.tag == "Rail") {
            transform.SetParent(rail);
            /*while (Equals(transform.localRotation, Quaternion.identity)) {
                //Quaternion.Angle(transform.localRotation, Quaternion.identity);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 10);
                //transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0), Space.Self);
            }*/
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
            transform.localRotation = Quaternion.identity;
            transform.localScale =  new Vector3(0.7f / rail.localScale.x , 2, 4);
            //transform.GetComponent<ConfigurableJoint>().axis = transform.rotation.eulerAngles;
            //transform.GetComponent<ConfigurableJoint>().secondaryAxis = new Vector3(0, 1, 0);
        }
    }
}
