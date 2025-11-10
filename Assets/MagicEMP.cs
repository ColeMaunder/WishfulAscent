using UnityEngine;

public class MagicEMP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        GameObject entered = other.gameObject;
        if (entered.GetComponent<EMPOpen>())
        {
            entered.GetComponent<EMPOpen>().open();
        }
        if (entered.GetComponent<EMPLock>())
        {
            entered.GetComponent<EMPLock>().UnLock();
        }
    }
}
