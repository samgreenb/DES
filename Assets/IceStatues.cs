using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStatues : MonoBehaviour
{
    [SerializeField] List<GameObject> parts;
    [SerializeField] List<IceStatueEffect> effects;
    int partCounter = 0;

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
        Debug.Log("ISE :" + other.gameObject.name);
        if (!parts.Contains(other.gameObject)) return;
        other.gameObject.SetActive(false);
        partCounter++;
        if (partCounter == parts.Count) Activate();
    }

    private void Activate()
    {
        foreach(var effect in effects) 
        {
            effect.Activate();
        }
    }
}
