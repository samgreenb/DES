using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPlate : MonoBehaviour
{
    [SerializeField]
    GameObject pressed;
    [SerializeField]
    GameObject notPressed;
    [SerializeField]
    ClockHandle handle;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PushPull")) return;
        pressed.SetActive(true);
        notPressed.SetActive(false);
        handle.PlatePressed();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("PushPull")) return;
        pressed.SetActive(false);
        notPressed.SetActive(true);
        handle.PlateReleased();
    }

}
