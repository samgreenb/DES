using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHandle : MonoBehaviour
{
    [SerializeField] float clockSpeed = 1f;
    [SerializeField] float finalAngle = 60.0f;
    float stepAngle;
    int pressedPlates = 0;

    private void Start()
    {
        stepAngle = finalAngle / 3.0f;
    }

    public void PlatePressed()
    {
        StopAllCoroutines();
        pressedPlates++;
        StartCoroutine(nameof(moveHandle));
    }

    public void PlateReleased()
    {
        StopAllCoroutines();
        pressedPlates--;
        StartCoroutine(nameof(moveHandle));
    }

    IEnumerator moveHandle()
    {
        float startingAngle = transform.rotation.eulerAngles.z;
        float time = 0.0f;
        while (transform.rotation.eulerAngles.z != pressedPlates * stepAngle) {
            transform.rotation = Quaternion.Euler(0, 180, Mathf.LerpAngle(startingAngle, pressedPlates * stepAngle, time));
            time += Time.deltaTime * clockSpeed;
            yield return null;
        }
    }
}
