using UnityEngine;
using System.Collections;

public class GravityFeald : MonoBehaviour
{
    [SerializeField]
    float[] gravMods = {-1, 0, 1, 2f };
    [SerializeField]
    GameObject[] particleEffects;
    [SerializeField]
    Material[] materials;
    [SerializeField]
    int timeMode = 2;
    MeshRenderer materialRenderer;
    void Start()
    {
        materialRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    public void SetGravMode(int neweMode)
    {
        timeMode = neweMode;
        materialRenderer.material = materials[timeMode];
        particleEffects[timeMode].SetActive(true);
        SetParticles(neweMode);
    }
    public void Scroll(float inVal)
    {
        if (inVal > 0)
        {
            if (2 >= timeMode + 1)
            {
                timeMode++;
                materialRenderer.material = materials[timeMode];
                particleEffects[timeMode-1].SetActive(false);
                particleEffects[timeMode].SetActive(true);
            }
        }
        else
        {
            if (0 <= timeMode - 1)
            {
                timeMode--;
                materialRenderer.material = materials[timeMode];
                particleEffects[timeMode+1].SetActive(false);
                particleEffects[timeMode].SetActive(true);
            }
        }
    }
    private void SetParticles(int id) {
        foreach (GameObject pObject in particleEffects){
            if(pObject == particleEffects[id]){
                pObject.SetActive(true);
            }else{
                pObject.SetActive(false);
            }
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<TimeForce>() != null)
        {
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            tf.SetGravMod(gravMods[timeMode]);

            //Item item = collision.gameObject.GetComponent<Item>();
            //item.SetTimeMod(forceMods[timeMode]);
            //rb.linearVelocity = rb.linearVelocity * 0;
            //rb.angularVelocity = rb.angularVelocity * 0;
            //rb.Sleep();
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<TimeForce>() != null)
        {
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            tf.SetGravMod(1);
        }
    }
}

