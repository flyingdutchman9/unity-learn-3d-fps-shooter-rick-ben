using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorOpen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInChildren<DoorRotation>().OpenDoor();
    }
}
