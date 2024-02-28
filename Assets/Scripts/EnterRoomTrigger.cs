using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> sniffItems;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        foreach (GameObject hiddenItem in sniffItems) {
            other.gameObject.GetComponent<DogController>().AddSniffArea(hiddenItem);
        }
         
    }
}
