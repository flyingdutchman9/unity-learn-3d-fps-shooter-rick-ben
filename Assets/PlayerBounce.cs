using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using static UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController;

public class PlayerBounce : MonoBehaviour
{
    [SerializeField] float force = 1200f;
    Rigidbody rb;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trampoline")
        {
            rb.velocity = Vector3.up * force * Time.deltaTime;
            RigidbodyFirstPersonController rbFPSCont = rb.transform.GetComponent<RigidbodyFirstPersonController>();
            //rbFPSCont.SetBounceValues(false);
            rbFPSCont.StartBounce();
        }
    }
}
