using UnityEngine;
using System.Collections;

public class MagicPowerd : MonoBehaviour
{
    [SerializeField] private bool empowerd = false;
    [SerializeField] float fealdScaleBase = 0.2f;
    [SerializeField] float fealdScaleMax = 10f;
    [SerializeField] float activeTime = 10f;
    [SerializeField] Material [] metirals;
    private float fealdScale = 0.2f;
    public float scaleMod = 10;
    [SerializeField] GameObject magicFeald;
    [SerializeField] MeshRenderer MagRenderer;
    Coroutine runEMP;
    
    public void Empower() {
        empowerd = true;
        swapMetrirals(1, metirals[0]);
    }
    public bool GetEmpowered() {
        return empowerd;
    }
    void swapMetrirals(int slot, Material newMet){
        Material[] activeMetirials = MagRenderer.materials;
        activeMetirials[slot] = newMet;
        MagRenderer.materials = activeMetirials;
    }
    public void DeStabolize() {
        if(empowerd){
            runEMP = StartCoroutine(EMP());
            empowerd = false;
        }
    }
    private IEnumerator EMP() {
        swapMetrirals(1, metirals[1]);
        magicFeald.SetActive(true);
        magicFeald.transform.localScale = scale(fealdScaleBase);
        fealdScale = fealdScaleBase;
        while (fealdScale < fealdScaleMax) {
            yield return new WaitForSeconds(0.01f);
            fealdScale += fealdScale / scaleMod;
            if (fealdScale > fealdScaleMax) {
                fealdScale = fealdScaleMax;
            }
            magicFeald.transform.localScale = scale(fealdScale);
        }
        yield return new WaitForSeconds(activeTime);
        magicFeald.SetActive(false);
     }
     Vector3 scale(float mod){
        return new Vector3(mod / transform.localScale.x, mod / transform.localScale.y, mod / transform.localScale.z);
     }
}
