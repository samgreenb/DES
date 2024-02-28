using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItemAudio : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event pickupAndDropSound = new AK.Wwise.Event();
    [SerializeField]
    private AK.Wwise.Event groundCollideSound = new AK.Wwise.Event();

    private Rigidbody rb;
    private bool falling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PickupAndDropBox()
    {
        pickupAndDropSound.Post(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        //if (rb.velocity.y <= 0.1f)
        if (falling)
        {
            groundCollideSound.Post(gameObject);
        }
    }

    /// Used to set our falling variable.
    void OnCollisionStay(Collision collision)
    {
        falling = false;
    }

    /// Used to set our falling variable.
    void OnCollisionExit(Collision collision)
    {
        falling = true;
    }
} 
