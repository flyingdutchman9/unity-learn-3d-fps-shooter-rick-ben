using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    List<GameObject> weapons;
    int currentWeaponIndex = 0;
    const string NotWeaponTag = "NotWeapon";

    private void Start()
    {
        LoadWeapons();
        SetActiveWeapon();
    }

    private void LoadWeapons()
    {
        weapons = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.tag != NotWeaponTag)
            {
                weapons.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        Weapon weapon = weapons[currentWeaponIndex].GetComponent<Weapon>();
        // drukcije dobijemo bug da se nece animacija odzumirati
        if (!weapon.IsWeaponZoomedIn())
        {
            ProcessKeyInput();
            ProcessScrollWheel();
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            SetActiveWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
            SetActiveWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeaponIndex = 2;
            SetActiveWeapon();
        }
    }

    private void ProcessScrollWheel()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScroll > 0)
        {
            currentWeaponIndex++;
            if (currentWeaponIndex > weapons.Count - 1) currentWeaponIndex = 0;
            SetActiveWeapon();
        }
        else if (mouseScroll < 0)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Count - 1;
            SetActiveWeapon();
        }
    }

    private void SetActiveWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[currentWeaponIndex].SetActive(true);
    }

}
