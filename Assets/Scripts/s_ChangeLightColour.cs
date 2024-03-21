using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_ChangeLightColour : MonoBehaviour
{

    public Light SelectedLight;
    public Color NewColor;
    public Quaternion NewRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        SelectedLight.color = NewColor;
        SelectedLight.transform.rotation = NewRotation;
    }

}
