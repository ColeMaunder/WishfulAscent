using UnityEngine;

public class EMPLock : MonoBehaviour
{
    [SerializeField] GameObject Lock;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void UnLock(){
        Lock.SetActive(false);
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.WakeUp();
    }
}
