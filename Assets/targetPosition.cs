using UnityEngine;

public class targetPosition : MonoBehaviour
{
    [SerializeField] Transform follow;
    [SerializeField] bool rotation;


    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position;
        if(rotation){
            transform.rotation = follow.rotation;
        }
        
    }
}
