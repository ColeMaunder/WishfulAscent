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
                    gravOrb.transform.SetParent(camra);
                    gravOrb.transform.localPosition = orbHold;
                }
            }else{
                orbBody.Sleep();
            }
        }
        
    }
    public void ToggleActive(InputAction.CallbackContext context){
        gravOrb.transform.GetChild(0).gameObject.SetActive(context.performed);
    }
}
