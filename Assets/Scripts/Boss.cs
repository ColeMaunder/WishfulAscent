using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
public class Boss : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int health = 3;
    [SerializeField] float speed = 3;
    TimeForce force;
    int lastHealth = 3;
    BossPathing pathing;
    [SerializeField] public bool hitPoint = false;
    [SerializeField] bool landed = false;
    Coroutine cycle;
    void Start() {
        pathing = FindFirstObjectByType<BossPathing>();
        force = GetComponent<TimeForce>();
        //cycle = StartCoroutine(BossCycle());
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0) {
            if (lastHealth == health) {
                if (!hitPoint) {
                    Vector3 directionToTarget = (target.position - transform.position).normalized;
                    force.AddLValocoty(directionToTarget * speed);
                    //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                } else {
                    if (landed) {
                        pathing.NewPoint();
                        landed = false;
                        hitPoint = false;
                    }
                }
                
            } else { 
                lastHealth--;
            }
        }
    }
    private IEnumerator BossCycle() {
        while(health > 0) {
            while(lastHealth == health) {
                pathing.NewPoint();
                yield return new WaitForSeconds(0.01f);
                 transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            lastHealth--;
        }
        yield return new WaitForSeconds(0.01f);
    }
    void OnTriggerEnter(Collider collision) {
        //if(collision.gameObject.name != "Goll Pont"){
        if(collision.gameObject.name == "Ground"){
            landed = true;
        }
    }
}
