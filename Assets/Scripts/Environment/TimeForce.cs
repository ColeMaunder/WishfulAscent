using Unity.VisualScripting;
using UnityEngine;

public class TimeForce : MonoBehaviour
{
    [SerializeField] bool gravityAfected = true;
    bool gravity = true;
    [SerializeField]
    private Vector3 baseGravityForce = new Vector3(0, -9.8f, 0);
    private Vector3 gravityForce;
    float gravMod = 1;
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gravityForce = baseGravityForce;
    }

    void FixedUpdate()
    {    
        if(gravMod == 0){
            rb.Sleep();
        }   
        if(gravityAfected && gravity){
            rb.AddForce(gravityForce * gravMod, ForceMode.Acceleration);
        }
    }
    public void SetGravMod(float newMod) {
        if (newMod == -1) {
            Gravity(false);
            rb.WakeUp();
        } else {
            Gravity(true);
            gravMod = newMod;  
        }
    }
    
    public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force){
        //rb.AddForce(force * timeMod, mode);
        rb.AddForce(force, mode);
    }
    public void AddTorque(Vector3 force, ForceMode mode = ForceMode.Force){
        //rb.AddTorque(force * timeMod, mode);
        rb.AddTorque(force , mode);
    }
    public void AddAValocity(Vector3 force){
        //rb.angularVelocity = force * mode;
        rb.angularVelocity = force;
    }
    public void AddLValocoty(Vector3 force){
        //rb.linearVelocity = force * mode;
        rb.linearVelocity = force;
    }
    
    public void Sleep() {
        rb.Sleep();
    }
    public void Gravity(bool sinState) {
        gravity = sinState;
    }
}
