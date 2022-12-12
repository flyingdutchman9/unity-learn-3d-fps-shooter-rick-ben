using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateHandler : MonoBehaviour
{
    [SerializeField] GameObject gateOpenSwitch;
    [SerializeField] GameObject gateCloseSwitch;
    [SerializeField] GameObject frontGate;
    
    [SerializeField] AudioClip gateOpenCloseSound;

    Light gateOpenLight;
    Light gateCloseLight;
    PlayerHealth player;

    float minDistanceFromSwitch = 2.5f;
    float maxAngleAgainstSwitch = 23f;
    Animator gateAnimator;

    private void Awake()
    {
        gateAnimator = frontGate.GetComponent<Animator>();
    }

    void Start()
    {
        gateOpenLight = gateOpenSwitch.transform.GetComponentInChildren<Light>();
        gateCloseLight = gateCloseSwitch.transform.GetComponentInChildren<Light>();
        gateOpenLight.enabled = true;
        gateCloseLight.enabled = false;
        player = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (gateOpenLight.enabled)
        {
            if (CanPlayerClickOpenSwitch() && Input.GetKeyDown(KeyCode.E))
            {
                OpenGatesAnimation();
                SwapSwitchLights();
                GetComponent<AudioSource>().PlayOneShot(gateOpenCloseSound);
            }
        }
        else if (gateCloseLight.enabled)
        {
            if (CanPlayerClickCloseSwitch() && Input.GetKeyDown(KeyCode.E))
            {
                CloseGatesAnimation();
                SwapSwitchLights();
                GetComponent<AudioSource>().PlayOneShot(gateOpenCloseSound);
            }
        }
        
    }

    private bool CanPlayerClickOpenSwitch()
    {
        float distanceFromUp = Vector3.Distance(player.transform.position, gateOpenSwitch.transform.position);
        var playerToUpSwitchAngle = Vector3.Angle(player.transform.forward, gateOpenSwitch.transform.position - player.transform.position);

        return distanceFromUp < minDistanceFromSwitch && playerToUpSwitchAngle < maxAngleAgainstSwitch;
    }

    private bool CanPlayerClickCloseSwitch()
    {
        float distanceFromDown = Vector3.Distance(player.transform.position, gateCloseSwitch.transform.position);
        var playerToDownSwitchAngle = Vector3.Angle(player.transform.forward, gateCloseSwitch.transform.position - player.transform.position);

        return distanceFromDown < minDistanceFromSwitch && playerToDownSwitchAngle < maxAngleAgainstSwitch;
    }

    private void SwapSwitchLights()
    {
        gateOpenLight.enabled = !gateOpenLight.enabled;
        gateCloseLight.enabled = !gateCloseLight.enabled;
    }

    private void OpenGatesAnimation()
    {
        gateAnimator.SetBool("open_gates", true);
    }

    private void CloseGatesAnimation()
    {
        gateAnimator.SetBool("open_gates", false);
    }

}
