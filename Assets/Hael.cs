using UnityEngine;
using UnityEngine.InputSystem;

public class Hael : MonoBehaviour
{
    public GameObject gravOrb;
    public Transform camra;
    public float trowForce = 1000f;
    public float dropVeriance = 0.5f;
    private FPController controller;
    private Rigidbody orbBody;
    private Vector3 orbHold;
    void Start()
    {
        controller = transform.parent.gameObject.GetComponent<FPController>();
        //orbHold = new Vector3(0, -0.15f, 0.7f);
        orbHold = gravOrb.transform.localPosition;
        orbBody = gravOrb.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(gravOrb.transform.parent != null && Vector3.Distance(gravOrb.transform.localPosition, orbHold) > dropVeriance){
            gravOrb.transform.SetParent(null);
            orbBody.Sleep();
        }
    }
    public void GravOrbControll(InputAction.CallbackContext context){
        if(transform == controller.GetActiveCharicter()){
            if (context.performed) {
                if (gravOrb.transform.parent != null) {
                    gravOrb.transform.SetParent(null);
                    orbBody.AddForce(camra.forward * trowForce);
                } else {
                    //Vector3 direction = Vector3.Normalize(camra.position - gravOrb.transform.position);
                    //orbBody.AddForce(direction  *  trowForce);
                    gravOrb.transform.SetParent(camra);
                    gravOrb.transform.localPosition = orbHold;
                }
            }else{
                //if (gravOrb.transform.parent != null) {
                    //gravOrb.transform.localPosition = orbHold;
                //}
                    orbBody.Sleep();
            }
        }
        
    }
    public void ToggleActive(InputAction.CallbackContext context){
        if(transform == controller.GetActiveCharicter()){
            gravOrb.transform.GetChild(0).gameObject.SetActive(context.performed);
        }
    }
}
