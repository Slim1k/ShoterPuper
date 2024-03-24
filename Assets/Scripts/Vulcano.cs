using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcano : MonoBehaviour
{
    public GameObject GrenadePrefab;
    public float force = 500;
    public float delayMin = 1;
    public float delayMax = 3;

    private void Start()
    {
        Invoke("SpawnGrenade", Random.Range(delayMin, delayMax));
    }

    private void SpawnGrenade()
    {
        var grenade = Instantiate(GrenadePrefab);
        grenade.transform.position = transform.position;

        var direction = Random.insideUnitCircle;

        grenade.GetComponent<Rigidbody>().AddForce(direction * force);
        Invoke("SpawnGrenade", Random.Range(delayMin, delayMax));
    }
}
