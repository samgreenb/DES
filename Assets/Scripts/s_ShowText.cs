using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class s_ShowText : MonoBehaviour
{

    public GameObject Text;
    public GameObject Player;
    public bool DoOnce = false;


    private void OnTriggerEnter(Collider other)
    {

        if (DoOnce != true)
        {
            if (!other.CompareTag("Player")) return;
            Text.SetActive(true);
            Invoke("Delay", 2);
            DoOnce = true;
        }


    }


    private void Delay()
    {
        Text.SetActive(false);

    }
}
