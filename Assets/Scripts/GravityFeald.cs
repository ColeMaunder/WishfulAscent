using UnityEngine;
using System.Collections;
using TreeEditor;

public class GravityFeald : MonoBehaviour
{
    [SerializeField]
    float[] gravMods = {-1, 0, 1, 2f };
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
    }
    public void Scroll(float inVal)
    {
        if (inVal > 0)
        {
            if (3 >= timeMode + 1)
            {
                timeMode++;
                materialRenderer.material = materials[timeMode];
            }
        }
        else
        {
            if (0 <= timeMode - 1)
            {
                timeMode--;
                materialRenderer.material = materials[timeMode];
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

