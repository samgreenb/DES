using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBreak : MonoBehaviour
{
    [SerializeField]
    GameObject hiddenObject;

    [SerializeField]
    float breakVelocity;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(rb.velocity.magnitude > breakVelocity)
        {
            Destroy(gameObject);
            hiddenObject.transform.position = transform.position;
            hiddenObject.transform.rotation = transform.rotation;
            hiddenObject.GetComponent<Rigidbody>().velocity = rb.velocity;
            hiddenObject.SetActive(true);
        }
    }
}
