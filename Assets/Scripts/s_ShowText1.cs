using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class s_ShowText1 : MonoBehaviour
{

    public GameObject Text;
    public GameObject Player;
    [SerializeField] List<KeyCode> KeyCodesNeeded = new();
    private bool DoOnce = false;


    private void OnTriggerEnter(Collider other)
    {

        if (DoOnce != true)
        {
            if (!other.CompareTag("Player")) return;
            Text.SetActive(true);
            DoOnce = true;
        }

    }


    private void Update()
    {
        for (int i = 0; i < KeyCodesNeeded.Count; i++)
        {
            if (Input.GetKeyDown(KeyCodesNeeded[i]))
            {
                Invoke("Delay", 2);
            }
        }
    }


    private void Delay()
    {
        Text.SetActive(false);

    }
}
