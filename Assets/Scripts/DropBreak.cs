using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBreak : MonoBehaviour
{
    [SerializeField]
    GameObject hiddenObject;

    [SerializeField]
    float breakVelocity;

    float yVelocity;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        yVelocity = rb.velocity.y;
        //Debug.Log(rb.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(yVelocity);
        if(yVelocity < breakVelocity)
        {
            Destroy(gameObject);
            hiddenObject.transform.position = transform.position;
            hiddenObject.transform.rotation = transform.rotation;
            hiddenObject.GetComponent<Rigidbody>().velocity = rb.velocity;
            hiddenObject.SetActive(true);
        }
    }
}
