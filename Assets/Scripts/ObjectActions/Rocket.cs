using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] ParticleSystem fireWorks;
    [SerializeField] GameObject explosion1;
    [SerializeField] GameObject explosion2;
    [SerializeField] ParticleSystem afterBurnerEffect;
    [SerializeField] Ammo ammo;

    Rigidbody _rigidbody;
    float thrustForce = 46000.0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ActivateLaunchSequence()
    {
        FireRocket();
        GetComponent<AudioSource>().Play();
        Invoke("ActivateFireworks", 5f);
        Destroy(gameObject, 5f);
    }

    private void FireRocket()
    {
        if (afterBurnerEffect != null)
        {
            //afterBurnerEffect.SetActive(true);
            afterBurnerEffect.Play();
        }
        else
        {
            print("AfterBurnerEffect on Rocket script is null");
        }

        _rigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
    }

    private void ActivateFireworks()
    {
        ExplodeRocket();
        IncreaseAllAmmo();

        if (fireWorks != null)
        {
            var fireWorksInstance = Instantiate(fireWorks, gameObject.transform.position, Quaternion.identity);
            fireWorksInstance.Play();
        }
        else
        {
            print("Fireworks on Rocket script is null");
        }
    }

    private void ExplodeRocket()
    {
        if (explosion1 != null)
        {
            var ex1 = Instantiate(explosion1, gameObject.transform.position, Quaternion.identity);
            ex1.SetActive(true);
        }
        else
        {
            print("Explosion 1 on Rocket script is null");
        }

        if (explosion2 != null)
        {
            var ex2 = Instantiate(explosion2, gameObject.transform.position, Quaternion.identity);
            ex2.SetActive(true);
        }
        else
        {
            print("Explosion 2 on Rocket script is null");
        }
    }

    private void IncreaseAllAmmo()
    {
        ammo.IncreaseCurrentAmmo(AmmoType.Ak47Bullet, 10000);
        ammo.IncreaseCurrentAmmo(AmmoType.PistolBullet, 10001);
        ammo.IncreaseCurrentAmmo(AmmoType.CarbineBullet, 10000);
    }
}
