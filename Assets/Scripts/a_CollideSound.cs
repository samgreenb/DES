using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_CollideSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event collideSound = new AK.Wwise.Event();

    private Rigidbody rb;
    private bool falling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //if (rb.velocity.y <= 0.1f)
        if (falling)
        {
            Debug.Log("Trying this thing");
            collideSound.Post(gameObject);
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
