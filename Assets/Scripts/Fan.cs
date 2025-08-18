using UnityEngine;

public class Fan : MonoBehaviour
{
    TimeForce tf;
    [SerializeField] float spinFore = 40;
    void Start(){
        tf = transform.gameObject.GetComponent<TimeForce>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddRelativeTorque(new Vector3(0, 0, 500),ForceMode.Force);
        tf.AddTorque(new Vector3(0, 0, spinFore),ForceMode.Force);
    }
}
