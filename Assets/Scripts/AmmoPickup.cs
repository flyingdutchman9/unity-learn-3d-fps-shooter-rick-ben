using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound;
    [SerializeField] AmmoType ammoType;

    private string playerTag = "Player";
    private bool canPickup = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            PlayPickupSound();
            IncreaseAmmo(other);
            canPickup = false;
            DisableMeshRenderer();
            Destroy(gameObject, 1.5f);
        }
    }

    private void IncreaseAmmo(Collider other)
    {
        other.GetComponent<Ammo>().IncreaseCurrentAmmo(ammoType);
    }

    private void DisableMeshRenderer()
    {
        var meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }

    private void PlayPickupSound()
    {
        GetComponent<AudioSource>().PlayOneShot(pickupSound);
    }

}
