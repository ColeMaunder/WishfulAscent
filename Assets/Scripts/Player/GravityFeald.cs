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
    int timeMode = 1;
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
    public void GravToggle(bool fealdEnabled)
    {
        if (fealdEnabled) {
            timeMode = 0;
        } else {
            timeMode = 1;
        }
        materialRenderer.material = materials[timeMode];
        particleEffects[timeMode].SetActive(true);
        SetParticles(timeMode);
    }
    public void Scroll(float inVal)
    {
        if (inVal > 0)
        {
            if (1 >= timeMode + 1)
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
        if (collision.gameObject.GetComponent<MagicPowerd>() != null && gravMods[timeMode] != 1)
        {
            MagicPowerd mp = collision.gameObject.GetComponent<MagicPowerd>();
            mp.DeStabolize();
        }
    }
    /*void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<TimeForce>() != null)
        {
            if(timeMode == 0){
                TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
                tf.Sleep();
            }
            

            //Item item = collision.gameObject.GetComponent<Item>();
            //item.SetTimeMod(forceMods[timeMode]);
            //rb.linearVelocity = rb.linearVelocity * 0;
            //rb.angularVelocity = rb.angularVelocity * 0;
            //rb.Sleep();
        }
    }*/
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<TimeForce>() != null)
        {
            TimeForce tf = collision.gameObject.GetComponent<TimeForce>();
            tf.SetGravMod(1);
        }
    }
}

