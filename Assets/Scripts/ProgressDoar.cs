using UnityEngine;

public class ProgressDoar : MonoBehaviour
{
    [SerializeField] int progressRequired;
    [SerializeField] float speed = 2;
    [SerializeField] Vector3 openPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != openPoint && Saving.activeSave.roomPrgress >= progressRequired){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, openPoint, speed * Time.deltaTime);
        }
    }
}
