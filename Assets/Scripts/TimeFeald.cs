using UnityEngine;

public class TimeFeald : MonoBehaviour
{
    [SerializeField]
    float[] forceMods = { 0, 0.5f, 1, 2f };
    [SerializeField]
    int timeMode = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Scroll(float inVal){
        if (inVal > 0){
            if (forceMods.Length > timeMode +1){
                timeMode++;
            }
        }else{
            if (0 <= timeMode -1){
                timeMode--;
            }
        }
    }
    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.GetComponent<TimeForce>() != null) {
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            tf.SetTimeMod(forceMods[timeMode]);

            //Item item = collision.gameObject.GetComponent<Item>();
            //item.SetTimeMod(forceMods[timeMode]);
            //rb.linearVelocity = rb.linearVelocity * 0;
            //rb.angularVelocity = rb.angularVelocity * 0;
            //rb.Sleep();
        }
    } 
    void OnTriggerExit(Collider collision) {
        if (collision.gameObject.GetComponent<TimeForce>() != null) {
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            tf.SetTimeMod(1);
        }
    } 
}

