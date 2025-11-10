using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using Unity.VisualScripting;
public class Boss : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int health = 3;
    [SerializeField] float speed = 3;
    TimeForce force;
    int lastHealth = 3;
    BossPathing pathing;
    [SerializeField] public bool hitPoint = false;
    [SerializeField] bool abzorbed = false;
    [SerializeField] bool landed = false;
    [SerializeField] bool caught = false;
    Coroutine cycle;
    Animator cas;
    Rigidbody rb;
    void Start() {
        pathing = FindFirstObjectByType<BossPathing>();
        force = GetComponent<TimeForce>();
        rb = GetComponent<Rigidbody>();
        cas = transform.GetChild(0).gameObject.GetComponent<Animator>();
        //cycle = StartCoroutine(BossCycle());
    }

    // Update is called once per frame
    void Update()
    {
        if (Saving.activeSave.roomPrgress >= 1)
        {
            if (health > 0)
            {
                if (lastHealth == health)
                {

                    if (!hitPoint)
                    {
                        if (!caught)
                        {
                            cas.SetBool("Move", true);
                            Vector3 directionToTarget = (target.position - transform.position).normalized;
                            force.AddLValocoty(directionToTarget * speed);
                            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, speed * Mathf.Rad2Deg * Time.fixedDeltaTime));
                            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                        }
                        else
                        {
                            cas.SetBool("Move", false);
                            force.Sleep();
                        }
                    }
                    else
                    {
                        if (landed)
                        {
                            pathing.NewPoint();
                            landed = false;
                            hitPoint = false;
                        }
                    }

                }
                else
                {
                    Saving.activeSave.roomPrgress++;
                    transform.position = target.parent.position;
                    lastHealth--;
                }
            }
        }
    }
    private IEnumerator BossCycle() {
        while(health > 0) {
            while(lastHealth == health) {
                if( !caught){
                    pathing.NewPoint();
                    yield return new WaitForSeconds(0.01f);
                    if(!abzorbed){
                        transform.position = Vector3.MoveTowards(transform.position, target.parent.position, speed / 2 * Time.deltaTime);
                        force.Gravity(false);
                    } else{
                        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                        force.Gravity(true);
                    }
                }else{
                    force.Sleep();
                }
            }
            lastHealth--;
            caught = false;
        }
        yield return new WaitForSeconds(0.01f);
    }
    void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.name != "Goll Pont"){
        if (collision.gameObject.name == "Ground")
        {
            landed = true;
        }
    }
    public void Emower()
    {
        abzorbed = true;
    }
    public void SetCaugh(bool state)
    {
        caught = state;
    }
    public void heart(){
        health--;
    }
}
