using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Nafre : MonoBehaviour
{
    private FPController controller;
    public GameObject timeFeald;
    public Transform camra;
    private float fealdScale = 0.2f;
    public float scaleMod = 10;
    public float fealdScaleBase = 0.2f;
    public float fealdScaleMax = 10f;
    public float maxReturnDistance = 10f;
    //Vector3 fealdHold;
    Transform holdPoint;
    private Coroutine scaleUp;

    void Start()
    {
        holdPoint = camra.GetChild(0);
        controller = transform.parent.gameObject.GetComponent<FPController>();
        fealdScale = fealdScaleBase;
    }
    void Update(){

    }
    public void TemporalContoll(InputAction.CallbackContext context){
        if(transform == controller.GetActiveCharicter()){
            if (context.performed) {
                if (fealdScale <= fealdScaleBase){
                    scaleUp = StartCoroutine(ScaleUpFeald());
                }else{
                    Vector3 fealdPosition = timeFeald.transform.position;
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
            } else {
                if( fealdScale > fealdScaleBase && scaleUp != null){
                    StopCoroutine(scaleUp);
                    timeFeald.transform.SetParent(null);
                }
            }
        }
    }
    /*public void ToggleActive(InputAction.CallbackContext context){
        if(transform == controller.GetActiveCharicter()){
            if(context.performed){
                if(timeFeald.transform.parent != null){
                    //timeFeald.transform.SetParent(null);
                }else{
                    timeFeald.transform.SetParent(camra);
                    timeFeald.transform.localPosition = fealdHold;
                    timeFeald.transform.localScale = new Vector3(fealdScaleBase, fealdScaleBase, fealdScaleBase);
                    fealdScale = fealdScaleBase;
                }
            }
        }
    }*/
    private IEnumerator ScaleUpFeald() {
        while(fealdScale < fealdScaleMax) {
            yield return new WaitForSeconds(0.01f);
            fealdScale += fealdScale/scaleMod;
            timeFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        timeFeald.transform.SetParent(null);
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
            timeFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        //timeFeald.transform.localPosition = fealdHold;
        while (Vector3.Distance(timeFeald.transform.position, holdPoint.position) != 0)
        {
            yield return null;
            timeFeald.transform.position = Vector3.MoveTowards(timeFeald.transform.position, holdPoint.position, 40 * Time.deltaTime);
        }
        timeFeald.transform.SetParent(camra);
     }
    public void ScrollTime(InputAction.CallbackContext context) {
        if (transform == controller.GetActiveCharicter()) {
            if (context.performed) {
                float value = context.ReadValue<float>();
                if (value != 0) {
                    print(value);
                    timeFeald.GetComponent<TimeFeald>().Scroll(value);
                }
            }
        }
    }
    public void TimeNoraml(InputAction.CallbackContext context) {
        if (transform == controller.GetActiveCharicter()) {
            if (context.performed) {
                timeFeald.GetComponent<TimeFeald>().Scroll(2);
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
        timeFeald.transform.SetParent(camra);
        timeFeald.transform.position = holdPoint.position;
        fealdScale = fealdScaleBase;
        timeFeald.transform.localScale = new Vector3(fealdScaleBase, fealdScaleBase, fealdScaleBase);
    }

}
