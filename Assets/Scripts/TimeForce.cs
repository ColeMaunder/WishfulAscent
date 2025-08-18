using UnityEngine;

public class TimeForce : MonoBehaviour
{
    [SerializeField] bool gravity = true;
    private Vector3 gravityForce = new Vector3(0, -9.81f, 0);
    float timeMod = 1;
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if( timeMod== 0){
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
                
        if(gravity){
            rb.AddForce(gravityForce * timeMod, ForceMode.Acceleration);
        }
    }
    public void SetTimeMod(float newMod) {
        timeMod = newMod;
    }
    
    public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force){
        rb.AddForce(force * timeMod, mode);
    }
    public void AddTorque(Vector3 force, ForceMode mode = ForceMode.Force){
        rb.AddTorque(force * timeMod, mode);
    }
    
    public void Sleep()
    {
        rb.Sleep();
    }
}
