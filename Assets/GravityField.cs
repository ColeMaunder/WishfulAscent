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
    void OnCollisionStay(Collision collision) {
        if(collision.gameObject.tag == "Object"){
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        }
    } 
}
