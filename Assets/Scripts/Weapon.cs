using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 30;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float fireRate = 5.5f;
    [SerializeField] RigidbodyFirstPersonController rigidbodyFirstPersonController;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip noAmmoSound;
    [SerializeField] float fireVolume = 0.5f;
    
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    public bool CanZoom;
    private bool isZoomed;
    AudioSource audioSource;
    
    float nextFire = 0.0f;
    private string[] triggerWeapons = new string[] { WeaponAnimationType.Shoot };
    private bool canShoot = true;

    internal void SetZoomInAnimation()
    {
        if (CanZoom)
        {
            GetComponent<Animator>().SetBool(WeaponAnimationType.ZoomIn, true);
            isZoomed = true;
        }
    }

    internal void SetZoomOutAnimation()
    {
        if (CanZoom)
        {
            GetComponent<Animator>().SetBool(WeaponAnimationType.ZoomIn, false);
            isZoomed = false;
        }
    }

    public bool IsWeaponZoomedIn()
    {
        return isZoomed;
    }

    // Promjenom oružja ovaj event resetira canShoot jer je skripta samo jedna instanca
    private void OnEnable()
    {
        ammoSlot = FindObjectOfType<Ammo>();
        SetCurrentAmmoText();
        canShoot = true;
    }

    //Pažnja: postoje animatori, ali nema animacija za oružja!!!! 
    private void Start()
    {
        audioSource = gameObject.GetComponentInParent<WeaponSwitcher>().GetComponent<AudioSource>();
    }

    void Update()
    {
        // Na žgance usporedbom vremena...možda korisno za neki drugi slučaj
        if (Input.GetButton("Fire1") && canShoot)// && Time.time > nextFire)
        {
            PlayFireSound();
            StartCoroutine(StartShooting());
        }
        //if (Input.GetButtonDown("Fire1") && GetComponent<Ammo>().GetAmmoAmount <= 0)
        //{
        //    PlayNoAmmoSound();
        //}
        //else if (Input.GetButton("Fire1") && canShoot)
        //{
        //    //StartCoroutine(FireWithDelay());
        //    FireWithDelay();
        //}
    }

    public void SetCurrentAmmoText()
    {
        ammoSlot = FindObjectOfType<Ammo>();
        ammoSlot.SetAmmoAmountText(ammoType);
    }

    private IEnumerator StartShooting()
    {
        canShoot = false;
        //ammoSlot = GetComponent<Ammo>();

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            //PlayFireSound();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayMuzzleFlash();
            Shoot();
        }
        else
        {
            PlayNoAmmoSound();
        }

        SetCurrentAmmoText();

        yield return new WaitForSeconds(fireRate);
        //nextFire = Time.time + fireRate;
        canShoot = true;
    }

    private void PlayNoAmmoSound()
    {
        audioSource.PlayOneShot(noAmmoSound);
    }

    private void PlayFireSound()
    {
        audioSource.PlayOneShot(fireSound);
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void Shoot()
    {
        //SetWeaponAnimation(WeaponAnimationType.Shoot);
        ProcessRaycast();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        // 0. Raycast vraća bool je li pogođeno što ili nije
        // 1. parametar - from -> odakle dolazi pucanj
        // 2. parametar - where to -> u kojem smjeru ide pucanj
        // 3. parametar - spremamo objekt koji je metak pogodio
        // 4. parametar - predefinirani parametar - koliko daleko metak ide
        bool didWeHitSomething = Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range);

        // Ispričavam se zbog ovoga...kao što rekoh, ideja je bila imati samo Deamon girl
        if (didWeHitSomething && hit.transform.tag == "Deamon")
        {
            //CreateHitImpact(hit);
            EnemyHealthDemon target = hit.transform.GetComponent<EnemyHealthDemon>();
            target?.TakeDamage(damage);
        }
        else if (didWeHitSomething && hit.transform.tag == "Zombie")
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            target?.TakeDamage(damage);
        }
        //else if (didWeHitSomething && hit.transform.tag == "Rocket")
        //{
        //    hit.transform.GetComponent<Rocket>().ActivateLaunch();
        //}

    }

    private void CreateHitImpact(RaycastHit hit)
    {
        // hit.normal znači da će efekt biti okrenut u smjeru od pogođenog objekta
        var effectObject = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effectObject, 0.5f);
    }

    //public void SetWeaponAnimation(string animationName)
    //{
    //    if (triggerWeapons.Contains(animationName))
    //    {
    //        GetComponent<Animator>().SetTrigger(animationName);
    //    }
    //    else
    //    {
    //        GetComponent<Animator>().SetBool(animationName, true);
    //    }
    //}

    
}
