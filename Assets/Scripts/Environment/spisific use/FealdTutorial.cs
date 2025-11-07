using UnityEngine;
using System.Collections;

public class FealdTutorial : MonoBehaviour
{
    GameObject platform;
    [SerializeField]
    GameObject orb;
    public GravityFeald gravFeald;
    [SerializeField]
    private float bounceForce;
    [SerializeField]
    private GameObject[] toDisable;
    [SerializeField]
    private float[] timings = { 0.5f, 0.5f, 1 };
    Luna luna;
    void Start()
    {
        luna = GameObject.FindWithTag("Player").transform.GetComponentInChildren<Luna>();
        StartCoroutine(TutorialLoop());
    }
    void Update(){
        if(luna.GetFlight()){
            //DisableAll();
        }
    }
    private IEnumerator TutorialLoop()
    {
        platform = transform.GetChild(0).gameObject;
        gravFeald = GameObject.FindWithTag("GravityFeald").gameObject.GetComponent<GravityFeald>();
        yield return new WaitForSeconds(0.5f);
/*        while (transform.GetChild(1) == gravFeald.transform)
        {
            gravFeald.SetGravMode(0);
            platform.GetComponent<TimeForce>().AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            yield return new WaitForSeconds(timings[0]);
            gravFeald.SetGravMode(1);
            yield return new WaitForSeconds(timings[1]);
        }
        orb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;*/
        
        /*while (true)
        {
            platform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            platform.gameObject.transform.localEulerAngles = new Vector3(30, 0, 0);
            platform.AddForce(new Vector3(0, 1, -1) * bounceForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.1f);
            platform.AddForce(new Vector3(0, 1, -1) * -bounceForce, ForceMode.Impulse);
            yield return new WaitForSeconds(timings[1]);
        }*/


    }
    void DisableAll(){
            foreach (GameObject i in toDisable) {
                i.SetActive(false);
            }
        }
}
