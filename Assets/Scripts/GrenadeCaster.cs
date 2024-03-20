using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCaster : MonoBehaviour
{
    public Rigidbody GrenadePrefab;
    public Transform GranadeSourceTransform;
    public float force = 10;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var grenade = Instantiate(GrenadePrefab);
            grenade.transform.position = GranadeSourceTransform.position;
            grenade.GetComponent<Rigidbody>().AddForce(GranadeSourceTransform.forward * force);
        }
    }
}
