using UnityEngine;
using System.Collections;

public class MagicDipencer : MonoBehaviour
{
    [SerializeField] GameObject droplet;
    Rigidbody dropRB;
    [SerializeField] bool dropping = true;
    [SerializeField] float startupTime = 1;
    ParticleSystem partcles;
    void Start()
    {
        dropRB = droplet.GetComponent<Rigidbody>();
        partcles = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update() {
        if (dropping && !droplet.activeSelf && !partcles.isPlaying) {
            droplet.transform.localPosition = Vector3.zero;
            droplet.GetComponent<TimeForce>().SetGravMod(1);
            droplet.SetActive(true);
            dropRB.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(drop());
        }
    }

    public void SetDropping(bool state){
        dropping = state;
    }
    IEnumerator drop() {
        yield return new WaitForSeconds(startupTime);
        dropRB.constraints = RigidbodyConstraints.None;
    }
}
