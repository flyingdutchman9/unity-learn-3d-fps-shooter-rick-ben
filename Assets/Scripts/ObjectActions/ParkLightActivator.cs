using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkLightActivator : MonoBehaviour
{
    [SerializeField] Camera camera1;
    List<Light> parkLights = new List<Light>();
    Light sunSource;
    
    private bool sunOn = true;
    private float SunIntensity
    {
        get
        {
            float intensity = sunOn == true ? 0 : 1;
            sunOn = !sunOn;
            return intensity;
        }
    }

    void Start()
    {
        sunSource = GameObject.Find("SunSource").GetComponent<Light>();
        
        var streetLamps = GameObject.FindGameObjectsWithTag("Park_lights");

        for (int i = 0; i < streetLamps.Length; i++)
        {
            AddLightsForLamp(streetLamps[i]);
        }

        // Set to off by defaul...strange, I know...
        SwitchLightsOnOff();
    }

    private void AddLightsForLamp(GameObject gameObject)
    {
        var lights = gameObject.GetComponentsInChildren<Light>();
        parkLights.AddRange(lights);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerNearFuseBox())
            {
                SwitchLightsOnOff();
                SwitchSunLight();
            }
        }
    }

    void SwitchSunLight()
    {
        sunSource.intensity = SunIntensity;
    }

    private bool IsPlayerNearFuseBox()
    {
        if (Vector3.Distance(gameObject.transform.position, camera1.transform.position) < 2)
        {
            return true;
        }

        return false;
    }

    private void SwitchLightsOnOff()
    {
        for (int i = 0; i < parkLights.Count; i++)
        {
            parkLights[i].enabled = !parkLights[i].enabled;
        }
    }
}
