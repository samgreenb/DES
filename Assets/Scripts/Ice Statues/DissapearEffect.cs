using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearEffect : IceStatueEffect
{
    [SerializeField]
    private AK.Wwise.Event meltSound = new AK.Wwise.Event();

    public override void Activate()
    {
        Debug.Log("playmeltsound");
        meltSound.Post(gameObject);
        Invoke(nameof(Hide), 1.0f);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
