using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class s_ShowOnce : MonoBehaviour
{

    public GameObject Image;
    public GameObject Player;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Image.SetActive(true);
    }



}
