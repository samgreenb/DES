using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_ChangeLightColour : MonoBehaviour
{

    public Light SelectedLight;
    public Color NewColor; 
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        SelectedLight.color = NewColor;
    }

}
