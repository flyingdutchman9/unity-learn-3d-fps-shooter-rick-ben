using System.Linq;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] TMP_Text ammoAmountText;
    [SerializeField] AmmoSlot[] ammoSlots;

    int ammoPickupCount = 20;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoCount = 0)
    {
        if (ammoCount > 0)
        {
            GetAmmoSlot(ammoType).ammoAmount += ammoCount;
        }
        else
        {
            GetAmmoSlot(ammoType).ammoAmount += ammoPickupCount;
        }

        SetAmmoAmountText(ammoType);
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        return ammoSlots.First(x => x.ammoType == ammoType);
    }

    public void SetAmmoAmountText(AmmoType ammoType)
    {
        ammoAmountText.text = GetAmmoSlot(ammoType).ammoAmount.ToString();
    }
}
