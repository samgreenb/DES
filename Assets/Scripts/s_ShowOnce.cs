using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class s_ShowOnce : MonoBehaviour
{

    public GameObject Image;
    public GameObject Player;
    public bool Show;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        {
            if (Show == true)
            {
                Image.SetActive(true);

            }
            else
            {
                Image.SetActive(false);
                Debug.Log("HellooWoo");
            }

        }

    }



}
