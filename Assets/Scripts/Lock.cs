using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject doar;

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if (other.gameObject.name == "Key")
        {
            doar.SetActive(false);
            print("Open");
        }
    }
}
