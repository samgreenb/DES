using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearEffect : IceStatueEffect
{
    public override void Activate()
    {
        Destroy(gameObject);
    }
}
