using UnityEngine;

public class EMPOpen : MonoBehaviour
{
   [SerializeField] int progressRequired;
    [SerializeField] float speed = 2;
    [SerializeField] Vector3 openPoint;
    bool shuldOpen = false;

    public void open()
    {
        shuldOpen = true;

    }
    void Update()
    {
        if(transform.localPosition != openPoint && shuldOpen){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, openPoint, speed * Time.deltaTime);
            Saving.activeSave.roomPrgress = 1;
        }
    }
    
}
