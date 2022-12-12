using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas impactCanvas;
    [SerializeField] float impactTime = 0.3f;

    void Start()
    {
        impactCanvas.enabled = false;
    }

    public void ShowDamageCanvas()
    {
        StartCoroutine(ShowImpact());
    }

    IEnumerator ShowImpact()
    {
        impactCanvas.enabled = true;
        yield return new WaitForSeconds(impactTime);
        DisableDamageCanvas();
    }

    public void DisableDamageCanvas()
    {
        impactCanvas.enabled = false;
    }
}
