using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera[] cameras;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = 9999.9f;
        int i = 1;
        foreach (var camera in cameras)
        {
            if((camera.transform.position - player.transform.position).magnitude < dist)
            {
                camera.Priority = i++;
                dist = (camera.transform.position - player.transform.position).magnitude;
            } else
            {
                camera.Priority = 0;
            }
        }
    }
}
