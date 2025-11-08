using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class MagicDroplet : MonoBehaviour
{
    [SerializeField] ParticleSystem partcles;
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GravityFeald>() == null) {
            if (collision.gameObject.GetComponent<MagicPowerd>() != null) {
                collision.gameObject.GetComponent<MagicPowerd>().empowerd = true;
            } else {
                quaternion  ClosionAngel =  Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
                partcles.transform.rotation = ClosionAngel * Quaternion.Euler(-90, 0, 0);
                partcles.Play();
            }
            gameObject.SetActive(false);
        }
        
    }

}
