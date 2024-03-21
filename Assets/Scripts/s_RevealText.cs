using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_RevealText : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject HiddenText;
    public GameObject Object;


    void Start()
    {
    }
    private void OnTriggerEnter(Collider Object)
    {
        Invoke("RevealText", 7);
    }

    private void RevealText()
    {
        HiddenText.SetActive(true);
    }
}
