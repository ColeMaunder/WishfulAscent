using UnityEngine;

public class BossPathing : MonoBehaviour
{

    Transform gollPoint;
    void Start() {
        gollPoint = transform.GetChild(0);
        NewPoint();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NewPoint(){
        gollPoint.localPosition = new Vector3(0, 0, Random.Range(1.5f, 11.5f));
        transform.eulerAngles = new Vector3(6, Random.Range(0, 360), 0);
    }
}
