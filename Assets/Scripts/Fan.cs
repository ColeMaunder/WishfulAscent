using UnityEngine;

public class Fan : MonoBehaviour
{
    TimeForce tf;
    Rigidbody rb;
    [SerializeField] GameObject wall;
    [SerializeField] float spinFore = 40;
    void Start() {
        tf = transform.gameObject.GetComponent<TimeForce>();
        rb = transform.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddRelativeTorque(new Vector3(0, 0, 500),ForceMode.Force);
        tf.AddTorque(new Vector3(spinFore, 0, 0),ForceMode.Force);
            wall.SetActive(rb.angularVelocity.magnitude != 0);
    }
}
