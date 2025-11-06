using UnityEngine;

public class ActivateAbilitys : MonoBehaviour
{
    [SerializeField]
    bool SolCheck = true;
    void OnTriggerEnter(Collider collision)
    {
        if(SolCheck) {
           if (collision.gameObject.GetComponent<Sol>() != null) {
                collision.gameObject.GetComponent<Sol>().ActivetAbility();
                gameObject.SetActive(false);
            } 
        } else{
           if (collision.gameObject.GetComponent<Luna>() != null) {
                collision.gameObject.GetComponent<Luna>().ActivetAbility();
                gameObject.SetActive(false);
            }  
        }
        
    }
}
