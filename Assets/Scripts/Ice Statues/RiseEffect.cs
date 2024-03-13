using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseEffect : IceStatueEffect
{
    [SerializeField]
    private AK.Wwise.Event riseSound = new AK.Wwise.Event();
    [SerializeField]
    private float length = 3.0f;
    [SerializeField]
    private float riseDistance = 3.0f;

    public override void Activate()
    {
        riseSound.Post(gameObject);
        Invoke(nameof(Rise), 0.5f);
    }

    private void Rise()
    {
        StartCoroutine(nameof(RiseCoroutine));
    }

    IEnumerator RiseCoroutine()
    {
        float startingPos = transform.position.y;
        float time = 0.0f;
        while (time/length <= 1.0f)
        {
            Debug.Log(Mathf.Lerp(startingPos, startingPos + riseDistance, time / length));
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(startingPos, startingPos + riseDistance, time / length), transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
