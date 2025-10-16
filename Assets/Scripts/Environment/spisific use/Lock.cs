using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject doar;
    public GameObject stears;

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if (other.gameObject.name == "Key")
        {
            doar.SetActive(false);
            stears.SetActive(true);
            print("Open");
        }
    }
}
