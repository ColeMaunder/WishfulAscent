using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class AutoRail : MonoBehaviour
{
    [SerializeField] bool powered = false;
    [SerializeField] bool go = false;
    [SerializeField] Transform turnPoint;
    [SerializeField] Transform nextpoint;
    public bool turn = false;
    float amount = 0f;
    void Start()
    {
        StartCoroutine(runCart());
    }
    void Update() {
        if (powered && go) {
            if(turn) {
                transform.RotateAround(turnPoint.localPosition, turnPoint.up, 1f * Time.deltaTime);
            } else {
                //transform.localPosition = new Vector3(transform.localPosition.x + -0.01f, transform.localPosition.y, transform.localPosition.z);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextpoint.localPosition, 1f * Time.deltaTime);
            }
        }
        

    }
    private IEnumerator runCart() {
        while (true){
            yield return new WaitForSeconds(0.01f);
            if (powered && go) {
                
            }
        }
     }
}
