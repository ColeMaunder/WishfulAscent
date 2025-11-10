using UnityEngine;

public class DropRail : MonoBehaviour
{
    [SerializeField] Collider cart;
    [SerializeField] Collider stopper;

    private void  OnTriggerEnter(Collider other){
        Transform inRail = other.transform;
        if (inRail.name == "Drop Rail"){
            Debug.Log("Cart can continue");
            Physics.IgnoreCollision( cart,stopper, true);
        }
    }
    private void OnTriggerExit(Collider other){
        Transform inRail = other.transform;
        if (inRail.name == "Drop Rail"){
            Physics.IgnoreCollision( cart, stopper, false);
        }
    }
    /*private void OnCollisionStay(Collision collision){
        Transform cart = collision.transform;
        if (cart.name == "Cart Boddy"){
            cart.localPosition = new Vector3(cart.localPosition.x -1, cart.localPosition.y, cart.localPosition.z);
        }
    }*/
}
