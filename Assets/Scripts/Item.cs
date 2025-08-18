using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Vector3 inateLinerValocity = new Vector3(0, 0, 0);
    [SerializeField]
    Vector3 inateAngularValocity = new Vector3(0, 0, 0);
    [SerializeField]
    float timeMod = 1;
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(inateLinerValocity != Vector3.zero){
            rb.linearVelocity = inateLinerValocity * timeMod;
        }
        if(inateAngularValocity != Vector3.zero){
            rb.angularVelocity = inateAngularValocity * timeMod;
        }
        
    }

}
