using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class Luna : MonoBehaviour
{
    [SerializeField]
    bool hasPowerControll = true;
    public bool flight = false;
    private FPController controller;
    private GameObject gravFeald;
    public Transform camra;
    private float fealdScale = 0.2f;
    public float scaleMod = 10;
    public float fealdScaleBase = 0.2f;
    public float fealdScaleMax = 10f;
    public float maxReturnDistance = 10f;
    //Vector3 fealdHold;
    Transform holdPoint;
    private Coroutine scaleUp;
    bool fealdEnabledLock = false;
    Animator animator;
    public bool GetFlight(){
        return flight;
    }

    void Start()
    {
        gravFeald = GameObject.FindWithTag("GravityFeald").gameObject;
        holdPoint = camra.GetChild(0);
        controller = transform.parent.GetComponent<FPController>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Update(){

        flight = gravFeald.transform.parent == camra;
    }
    
    public void TemporalContoll(InputAction.CallbackContext context){
        if (transform == controller.GetActiveCharicter() && hasPowerControll) {
            Debug.Log("Adara active");
            if (context.performed) {
                if (fealdScale <= fealdScaleBase) {
                    scaleUp = StartCoroutine(ScaleUpFeald());
                } else {
                    returnFeald();
                }
            } else {
                if (fealdScale > fealdScaleBase && scaleUp != null) {
                    StopCoroutine(scaleUp);
                    gravFeald.transform.SetParent(null);
                    //animator.SetBool("Released", true);
                }
            }
        }
    }
    /*public void ToggleActive(InputAction.CallbackContext context){
        if(transform == controller.GetActiveCharicter()){
            if(context.performed){
                if(gravFeald.transform.parent != null){
                    //gravFeald.transform.SetParent(null);
                }else{
                    gravFeald.transform.SetParent(camra);
                    gravFeald.transform.localPosition = fealdHold;
                    gravFeald.transform.localScale = new Vector3(fealdScaleBase, fealdScaleBase, fealdScaleBase);
                    fealdScale = fealdScaleBase;
                }
            }
        }
    }*/
    private void returnFeald() {
        Vector3 fealdPosition = gravFeald.transform.position;
        Vector3 direction = (holdPoint.position - fealdPosition).normalized;
        float distance = Vector3.Distance(fealdPosition , holdPoint.position);
        RaycastHit hit;
        if (distance > maxReturnDistance && Physics.Raycast(fealdPosition, direction, out hit, distance) && hit.collider.gameObject.GetComponent<TimeForce>() == null){
            Debug.Log("Orb return blocked by : " + hit.collider.gameObject.name);
            FealdRreset();
        } else {
            StartCoroutine(ScaleDownFeald());
        }
    }
    private IEnumerator ScaleUpFeald() {
        animator.SetBool("Released", true);
        while(fealdScale < fealdScaleMax) {
            yield return new WaitForSeconds(0.01f);
            fealdScale += fealdScale/scaleMod;
            if(fealdScale > fealdScaleMax){
                fealdScale = fealdScaleMax;
            }
            gravFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        gravFeald.transform.SetParent(null);
     }
    private IEnumerator ScaleDownFeald()
    {
        if (scaleUp != null){
            StopCoroutine(scaleUp);
            scaleUp = null;
        }
        while (fealdScale > fealdScaleBase)
        {
            yield return new WaitForSeconds(0.01f);
            fealdScale -= fealdScale / (scaleMod / 5);
            if (fealdScale < fealdScaleBase){
                fealdScale = fealdScaleBase;
            }
            gravFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        //gravFeald.transform.localPosition = fealdHold;
        while (Vector3.Distance(gravFeald.transform.position, holdPoint.position) != 0)
        {
            yield return null;
            gravFeald.transform.position = Vector3.MoveTowards(gravFeald.transform.position, holdPoint.position, 40 * Time.deltaTime);
        }
        yield return  new WaitForSeconds(0.01f);
        gravFeald.transform.SetParent(camra);
        animator.SetBool("Released", false);
        
     }
    public void ScrollTime(InputAction.CallbackContext context) {
        if (transform == controller.GetActiveCharicter() && hasPowerControll) {
            if (context.performed) {
                float value = context.ReadValue<float>();
                if (value != 0) {
                    print(value);
                    gravFeald.GetComponent<GravityFeald>().Scroll(value);
                }
            }
        }
    }
    public void GravToggle(InputAction.CallbackContext context) {
        if (transform == controller.GetActiveCharicter()  && hasPowerControll) {
            if (context.performed) {
                fealdEnabledLock = !fealdEnabledLock;
                gravFeald.GetComponent<GravityFeald>().GravToggle(fealdEnabledLock);
                animator.SetBool("Toggle", fealdEnabledLock);
            }
        }
    }
    public void GravHold(InputAction.CallbackContext context)
    {
        if (transform == controller.GetActiveCharicter() && hasPowerControll) {
            if (context.performed) {
                print("ok");
                gravFeald.GetComponent<GravityFeald>().GravToggle(true);
                animator.SetBool("Toggle", true);
            } else if (!fealdEnabledLock) {
                gravFeald.GetComponent<GravityFeald>().GravToggle(false);
                animator.SetBool("Toggle", false);
            }
        }
    }
    private void FealdRreset()
    {
        if (scaleUp != null)
        {
            StopCoroutine(scaleUp);
            scaleUp = null;
        }
        gravFeald.transform.SetParent(camra);
        gravFeald.transform.position = holdPoint.position;
        fealdScale = fealdScaleBase;
        gravFeald.transform.localScale = new Vector3(fealdScaleBase, fealdScaleBase, fealdScaleBase);
    }
    private IEnumerator ObtainOrb(){
        gravFeald.GetComponent<GravityFeald>().GravToggle(false);
        animator.SetBool("Toggle", false);
        fealdScale = 25;
        while (fealdScale > fealdScaleBase) {
            yield return new WaitForSeconds(0.01f);
            fealdScale -= fealdScale / (scaleMod / 5);
            if (fealdScale < fealdScaleBase){
                fealdScale = fealdScaleBase;
            }
            gravFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        yield return  new WaitForSeconds(0.5f);
        while (Vector3.Distance(gravFeald.transform.position, holdPoint.position) >0.001)
        {
            yield return new WaitForSeconds(0.0f);
            gravFeald.transform.position = Vector3.MoveTowards(gravFeald.transform.position, holdPoint.position, 30* Time.deltaTime);
        }
        gravFeald.transform.SetParent(camra);
    }
    public void ActivetAbility()
    {
        hasPowerControll = true;
        StartCoroutine(ObtainOrb());
    }
}
