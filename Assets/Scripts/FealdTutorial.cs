using UnityEngine;
using System.Collections;

public class FealdTutorial : MonoBehaviour
{
    TimeForce platform;
    [SerializeField]
    GameObject orb;
    public GravityFeald gravFeald;
    [SerializeField]
    private float bounceForce;
    [SerializeField]
    private GameObject[] toDisable;
    [SerializeField]
    private float[] timings = {0.5f,0.5f,1};

    void Start()
    {
        StartCoroutine(TutorialLoop());
    }

    private IEnumerator TutorialLoop()
    {
        GameObject platformOBJ = transform.GetChild(0).gameObject;
        platform = platformOBJ.GetComponent<TimeForce>();
        gravFeald = GameObject.FindWithTag("GravityFeald").gameObject.GetComponent<GravityFeald>();
        yield return new WaitForSeconds(0.5f);
        while (transform.GetChild(1) == gravFeald.transform)
        {
            gravFeald.SetGravMode(0);
            platform.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            yield return new WaitForSeconds(timings[0]);
            gravFeald.SetGravMode(1);
            yield return new WaitForSeconds(timings[1]);
            gravFeald.SetGravMode(2);
            yield return new WaitForSeconds(timings[2]);
        }
        orb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        foreach (GameObject i in toDisable)
        {
            i.SetActive(false);
        }
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
}
