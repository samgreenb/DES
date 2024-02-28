using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Collider area;
    [SerializeField] CinemachineVirtualCamera fCamera;
    [SerializeField] int priority;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) fCamera.Priority = priority;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) fCamera.Priority = 0;
    }
}
