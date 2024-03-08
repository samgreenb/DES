using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_CaveCollapse : MonoBehaviour
{
    public GameObject HiddenWall;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        HiddenWall.SetActive(true);
    }
}
