using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class StalactiteTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (var item in transform.parent.GetComponentsInChildren<StalactiteSpawn>())
        {
            item.TurnOn();
        }
    }
}
