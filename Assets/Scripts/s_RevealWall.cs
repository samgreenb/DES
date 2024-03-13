using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_RevealWall : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject HiddenWall;
    public GameObject Player;


    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        HiddenWall.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        HiddenWall.SetActive(false);
    }
}
