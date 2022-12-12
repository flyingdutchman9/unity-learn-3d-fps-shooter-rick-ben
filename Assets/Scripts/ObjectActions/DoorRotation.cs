using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    //public Transform door; //the door
    public float endRotation; //the end rotation
    public float startRotation; //the start rotation
    public float speed; //the speed the door opens

    public void OpenDoor()
    {
        StartCoroutine(OpenDoor(90, 100));
    }

    private IEnumerator OpenDoor(int targetAngle, int animationSpeed) //animates the door to [targetAngle] using amount of frames [animationSpeed]
    {
        for (int r = 0; r < animationSpeed; r += 1)
        {
            transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(transform.localEulerAngles.y, targetAngle, 5f / animationSpeed), 0);
            yield return null;
        }
    }

}
