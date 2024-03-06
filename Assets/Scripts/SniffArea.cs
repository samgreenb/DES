using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffArea : MonoBehaviour
{

    [SerializeField]
    GameObject hiddenObject;

    DogController dogData;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if(dogData.GetState() == STATE.Sniff) 
        {
            hiddenObject.SetActive(true);
            dogData.RemoveFromSniffArea(hiddenObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.gameObject;
        dogData = other.GetComponent<DogController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        player = null;
    }
}
