using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStatues : MonoBehaviour
{
    [SerializeField] List<GameObject> parts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("IEE :" + other.gameObject.name);
        //parts.fi other.gameObject
    }
}
