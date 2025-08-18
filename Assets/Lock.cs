using UnityEngine;

public class Lock : MonoBehaviour
{
    private Doar doar;
    void Start()
    {
        doar = GameObject.FindWithTag("Door").GetComponent<Doar>();
    }

    // Update is called once per frame
    public void OnCollisionEnter(Collision collision)
    {
        print("triggered");
        if (collision.gameObject.name == "Key")
        {
            doar.open();
            print("Open");
        }
    }
}
