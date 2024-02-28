using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbuttTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHeadbutted(float delay)
    {
        Invoke(nameof(Break), delay);
    }

    public void Break()
    {
        Destroy(gameObject);
    }
}
