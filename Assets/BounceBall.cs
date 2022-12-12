using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBall : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float force = 120f;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trampoline")
        {
            rb.velocity = Vector3.up * force * Time.deltaTime;
        }
    }
}
