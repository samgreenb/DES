using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_clickSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event clickSound = new AK.Wwise.Event();

    private void Start()
    {
        
    }

    public void PlaySound()
    {
        clickSound.Post(gameObject);
    }
}
