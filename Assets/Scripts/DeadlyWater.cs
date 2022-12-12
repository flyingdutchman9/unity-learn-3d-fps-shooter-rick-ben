using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyWater : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("KillPlayer", other);
        }
    }

    private IEnumerator KillPlayer(Collider playerCollider)
    {
        yield return new WaitForSeconds(3f);
        PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
        playerHealth.InstaKill("Water");
    }
}
