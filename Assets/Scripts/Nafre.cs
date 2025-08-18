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
    Vector3 fealdHold;
    private Coroutine scaleUp;

    void Start()
    {
        controller = transform.parent.gameObject.GetComponent<FPController>();
        fealdScale = fealdScaleBase;
        fealdHold = timeFeald.transform.localPosition;
    }
    void Update(){

    }
    public void TemporalContoll(InputAction.CallbackContext context){
        if(transform == controller.GetActiveCharicter()){
            if (context.performed) {
                if (fealdScale <= fealdScaleBase){
                    scaleUp = StartCoroutine(ScaleUpFeald());
                }else{
                    //timeFeald.transform.SetParent(camra);
                    //timeFeald.transform.localPosition = fealdHold;
                    //StopCoroutine(scaleUp);
                    StartCoroutine(ScaleDownFeald());
                    //timeFeald.transform.localScale = new Vector3(fealdScaleBase, fealdScaleBase, fealdScaleBase);
                    //fealdScale = fealdScaleBase;
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
        StopCoroutine(scaleUp);
        while (fealdScale > fealdScaleBase) {
            yield return new WaitForSeconds(0.01f);
            fealdScale -= fealdScale / (scaleMod / 5);
            timeFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        timeFeald.transform.SetParent(camra);
        //timeFeald.transform.localPosition = fealdHold;
        while (Vector3.Distance(timeFeald.transform.localPosition, fealdHold) != 0) {
            yield return new WaitForSeconds(0.01f);
            timeFeald.transform.localPosition = Vector3.MoveTowards(timeFeald.transform.localPosition, fealdHold, 20 * Time.deltaTime);
        }
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

}
