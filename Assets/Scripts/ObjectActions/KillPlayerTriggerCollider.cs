using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerTriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            ph.InstaKill("Box collider");
        }
    }
}
