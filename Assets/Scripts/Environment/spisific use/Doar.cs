using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
public class Doar : MonoBehaviour
{
    private float doarOpen = 0;
    private float amount = -4;
    [SerializeField] bool doOpen = false;
    bool run = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(doOpen && !run){
            StartCoroutine(ScaleUpFeald());
        }
    }
    public void open(){
        StartCoroutine(ScaleUpFeald());
    }
    private IEnumerator ScaleUpFeald() {
        run = true;
        while (amount < doarOpen){
            yield return new WaitForSeconds(1f);
           amount+= 0.0001f;
            transform.position = new Vector3(-9.5f, amount, 11);
        }
     }
}
