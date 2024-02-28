using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_WallTransparency : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ThingMaJig;
    public GameObject Player;


    void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ThingMaJig.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        ThingMaJig.SetActive(true);
    }
}
