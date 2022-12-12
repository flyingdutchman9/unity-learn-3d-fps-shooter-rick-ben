using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float zoomInSpeed = 0.01f;
    [SerializeField] float zoomOutSpeed = 0.2f;

    [SerializeField] float zoomInSensitivity = 0.2f;
    [SerializeField] float zoomOutSensitivity;

    RigidbodyFirstPersonController fpsController;

    float defaultFOV;
    float maxZoom;
    private bool izZoomed;

    private void Start()
    {
        defaultFOV = mainCamera.fieldOfView;
        maxZoom = defaultFOV / 2;
        fpsController = GetComponent<RigidbodyFirstPersonController>();
        zoomOutSensitivity = fpsController.mouseLook.YSensitivity;
    }

    private void Update()
    {
        Weapon activeWeapon = FindObjectOfType<Weapon>();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ZoomOut(activeWeapon);
        }
        else if (Input.GetMouseButtonDown(1)) //(Input.GetButtonDown("Fire2"))
        {
            if (activeWeapon.CanZoom)
            {
                if (!izZoomed)
                    ZoomIn(activeWeapon);
                else 
                    ZoomOut(activeWeapon);
            }
        }
        
        //else// if (Input.GetButtonUp("Fire2"))
        //{
        //    if (izZoomed && activeWeapon.CanZoom && defaultFOV != mainCamera.fieldOfView)
        //    {
        //        ZoomOut(activeWeapon);
        //    }
        //}


    }

    private void ZoomIn(Weapon activeWeapon)
    {
        izZoomed = true;
        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
        activeWeapon.SetZoomInAnimation();
        mainCamera.fieldOfView = maxZoom;
    }

    public void ZoomOut(Weapon activeWeapon)
    {
        izZoomed = false;
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
        activeWeapon.SetZoomOutAnimation();
        mainCamera.fieldOfView = defaultFOV;
    }
    //IEnumerator ZoomInAnimated(Weapon activeWeapon)
    //{
    //    fpsController.mouseLook.XSensitivity = zoomInSensitivity;
    //    fpsController.mouseLook.YSensitivity = zoomInSensitivity;
    //    activeWeapon.SetZoomInAnimation();

    //    while (mainCamera.fieldOfView > maxZoom)
    //    {
    //        mainCamera.fieldOfView -= zoomInSpeed;
    //        yield return null;
    //    }
    //}

    //IEnumerator ZoomOutAnimated(Weapon activeWeapon)
    //{
    //    fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
    //    fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    //    activeWeapon.SetZoomOutAnimation();

    //    while (mainCamera.fieldOfView < defaultFOV)
    //    {
    //        mainCamera.fieldOfView += zoomOutSpeed;
    //        yield return null;
    //    }
    //}

}
