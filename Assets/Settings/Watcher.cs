using UnityEngine;

public class Watcher : MonoBehaviour
{
    [SerializeField] string name;
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;
        if (enterObject.name == name) {
            gameObject.SetActive(false);
        }
    }
}
