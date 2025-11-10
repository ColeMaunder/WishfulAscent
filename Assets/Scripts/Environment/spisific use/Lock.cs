using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject stears;

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if (other.gameObject.name == "Key")
        {
            stears.SetActive(true);
            print("Open");
        }
    }
}
