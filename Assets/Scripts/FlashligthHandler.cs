using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashligthHandler : MonoBehaviour
{
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 40f;
    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        HandleInput();
        // Ako ćemo raditi na 
        //DecreaseLightAngle();
        //DecreaseLightIntensity();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            myLight.enabled = !myLight.enabled;
        }
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle > minimumAngle)
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }
}
