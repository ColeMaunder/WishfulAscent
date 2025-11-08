using UnityEngine;

public class targetPosition : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] bool rotation;
    [SerializeField] Vector3 offset = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        if (offset != Vector3.zero){
            transform.position = follow.position;
        } else {
           transform.position = follow.position + offset; 
        }
        if(rotation){
            transform.rotation = follow.rotation;
        }
        
    }
}
