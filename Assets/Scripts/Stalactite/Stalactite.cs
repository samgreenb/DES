using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event collideSound = new AK.Wwise.Event();

    private void Start()
    {
        Invoke(nameof(EnableCollider), 1.0f);
    }

    private void EnableCollider()
    {
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        Debug.Log("Trying this thing");
        collideSound.Post(gameObject);
        Destroy(gameObject);
    }
}
