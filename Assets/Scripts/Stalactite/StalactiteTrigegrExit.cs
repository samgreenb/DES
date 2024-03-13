using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteTrigegrExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Destroy(transform.parent.gameObject);
    }
}
