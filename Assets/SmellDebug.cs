using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellDebug : MonoBehaviour
{
    MeshRenderer sphere;
    DogController dog;
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.FindGameObjectWithTag("Player").GetComponent<DogController>();
        sphere = GetComponent<MeshRenderer>();
        sphere.enabled = false;
        sphere.material.color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        switch (dog.GetSniffState())
        {
            case SNIFFSTATE.NotSniff:
                sphere.enabled = false;
                break;
            case SNIFFSTATE.None:
                sphere.enabled = true;
                sphere.material.color = Color.black;
                break;
            case SNIFFSTATE.Close:
                sphere.enabled = true;
                sphere.material.color = Color.green;
                break;
            case SNIFFSTATE.Medium:
                sphere.enabled = true;
                sphere.material.color = Color.yellow;
                break;
            case SNIFFSTATE.Far:
                sphere.enabled = true;
                sphere.material.color = Color.red;
                break;
            case SNIFFSTATE.Found:
                sphere.enabled = true;
                sphere.material.color = Color.cyan;
                break;
        }
    }
}
