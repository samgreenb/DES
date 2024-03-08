using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_ShowText : MonoBehaviour
{

    public GameObject Text;
    public GameObject Player;
    public bool DoOnce = false;


    private void OnTriggerEnter(Collider other)
    {

        if (DoOnce == false);
        {
            if (!other.CompareTag("Player")) return;
            Text.SetActive(true);
            Invoke("Delay", 3);
        }

    }


    void Delay()
    {
        Text.SetActive(false);
        DoOnce = true;
    }
}
