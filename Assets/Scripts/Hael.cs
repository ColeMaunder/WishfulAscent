using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Hael : MonoBehaviour
{
    public GameObject gravOrb;
    public Transform camra;
    public float trowForce = 1000f;
    public float dropVeriance = 0.5f;
    private FPController controller;
    private TimeForce orbBody;
    private Vector3 orbHold;
    private bool hasChecked = false;
    private bool holding = false;
    private bool holdLocked = false;
    public float scaleMod = 5;
    private float fealdScale = 1f;
    public float fealdScaleBase = 1f;
    public float fealdScaleMax = 25f;
    private Coroutine scaleUp, scaleDown;
    Transform gravFeald;
    bool orbReturning = false;
    private List<Coroutine> orbReturnProsess = new List<Coroutine>();
    void Start()
    {
        controller = transform.parent.gameObject.GetComponent<FPController>();
        //orbHold = new Vector3(0, -0.15f, 0.7f);
        orbHold = gravOrb.transform.localPosition;
        orbBody = gravOrb.GetComponent<TimeForce>();
        gravFeald = gravOrb.transform.GetChild(0).GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        if (gravOrb.transform.parent != null && Vector3.Distance(gravOrb.transform.localPosition, orbHold) > dropVeriance)
        {
            gravOrb.transform.SetParent(null);
            orbBody.Sleep();
        }
    }
    public void GravOrbControll(InputAction.CallbackContext context)
    {
        if (transform == controller.GetActiveCharicter())
        {
            Rigidbody rb = gravOrb.transform.GetComponent<Rigidbody>();
            if (context.performed)
            {
                rb.constraints = RigidbodyConstraints.None;
                if (gravOrb.transform.parent != null)
                {
                    gravOrb.transform.SetParent(null);
                    orbBody.AddForce(camra.forward * trowForce);
                } else {
                    Vector3 orbHoldWorld = transform.TransformPoint(orbHold);
                    Vector3 orbPosition = orbBody.transform.position;
                    Vector3 direction = (orbHoldWorld - orbPosition).normalized;
                    float distance = Vector3.Distance(orbPosition , orbHoldWorld);
                    RaycastHit hit;

                    if (Physics.Raycast(orbPosition, direction, out hit, distance)  && hit.collider.gameObject.GetComponent<TimeForce>() == null){
                        Debug.Log("Orb return blocked by : " + hit.collider.gameObject.name);

                        wipeCorutene(orbReturnProsess);
                        orbReturning = false;
                        gravOrb.transform.SetParent(camra);
                        gravOrb.transform.localPosition = orbHold;
                    }else{
                        orbReturnProsess.Add(StartCoroutine(OrbBack(rb)));
                        Debug.Log("Orb Going to  : " + orbHoldWorld);
                    }
                    Debug.DrawRay(orbPosition, direction * distance, Color.red);

                    //Vector3 direction = Vector3.Normalize(camra.position - gravOrb.transform.position);
                    //orbBody.AddForce(direction  *  trowForce);

                    //StartCoroutine(OrbBack(rb));
                    //gravOrb.transform.SetParent(camra);
                    //gravOrb.transform.localPosition = orbHold;
                }
            }
            else
            {
                orbBody.Sleep();
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
        }

    }
    private void wipeCorutene(List<Coroutine> list)
    {
        foreach (Coroutine c in list){
            if (c != null){
                StopCoroutine(c);
            }
        }
        list.Clear();
    }
    public void ToggleActive(InputAction.CallbackContext context)
    {
        if (transform == controller.GetActiveCharicter())
        {
            if (context.performed && !hasChecked)
            {
                holdLocked = !holdLocked;
                if (!holding)
                {
                    if (holdLocked)
                    {
                        scaleUp = StartCoroutine(ScaleUpFeald());
                    }
                    else
                    {
                        scaleDown = StartCoroutine(ScaleDownFeald());
                    }
                }
                hasChecked = true;
            }
            else
            {
                hasChecked = false;
            }
        }
    }
    public void HoldActive(InputAction.CallbackContext context)
    {
        if (transform == controller.GetActiveCharicter())
        {
            if (context.performed)
            {
                print("ok");
                scaleUp = StartCoroutine(ScaleUpFeald());
            }
            else if (!holdLocked)
            {
                scaleDown = StartCoroutine(ScaleDownFeald());
            }
        }
    }

    private IEnumerator ScaleUpFeald()
    {
        if (scaleDown != null)
        {
            StopCoroutine(scaleDown);
        }
        gravFeald.gameObject.SetActive(true);
        while (fealdScale < fealdScaleMax)
        {
            yield return new WaitForSeconds(0.01f);
            fealdScale += fealdScale / scaleMod;
            if (fealdScale > fealdScaleMax) { fealdScale = fealdScaleMax; }
            gravFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
    }
    private IEnumerator ScaleDownFeald()
    {
        if (scaleUp != null)
        {
            StopCoroutine(scaleUp);
        }
        while (fealdScale > fealdScaleBase)
        {
            yield return new WaitForSeconds(0.01f);
            fealdScale -= fealdScale / scaleMod;
            if (fealdScale < fealdScaleBase) { fealdScale = fealdScaleBase; }
            gravFeald.transform.localScale = new Vector3(fealdScale, fealdScale, fealdScale);
        }
        gravFeald.gameObject.SetActive(false);
    }
    private IEnumerator OrbBack(Rigidbody rb)
    {
        if(!orbReturning){
            orbReturning = true;
            Vector3 orbHoldWorld = transform.TransformPoint(orbHold);
            while (Vector3.Distance(orbBody.transform.position, orbHoldWorld)> 0.5){
                orbHoldWorld = transform.TransformPoint(orbHold);
                yield return new WaitForSeconds(0.0f);
                //rb.MovePosition(orbHoldWorld * 0.001f * 10 * Time.deltaTime);
                orbBody.transform.position = Vector3.MoveTowards(orbBody.transform.position, orbHoldWorld, 5 * Time.deltaTime);
                print(gravOrb.transform.position);
            }
            gravOrb.transform.SetParent(camra);
            gravOrb.transform.localPosition = orbHold;
            orbReturning = false;
        }
     }
}