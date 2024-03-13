using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class StalactiteSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject stalactite;
    [SerializeField]
    private float frequency;
    [SerializeField]
    private int chance;

    public void TurnOn()
    {
        StartCoroutine(nameof(SpawnStalactites));
    }

    IEnumerator SpawnStalactites()
    {
        yield return new WaitForSeconds(Random.Range(0.0f,1.0f));
        while (true)
        {
            yield return new WaitForSeconds(frequency + Random.Range(-0.1f, 0.1f));
            if(Random.Range(1,100) <= chance) {
                Instantiate(stalactite, transform.position, Quaternion.identity);
            }
        }
    }
}
