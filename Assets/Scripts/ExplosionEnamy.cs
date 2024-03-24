using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnamy : MonoBehaviour
{
    public Explosion ExplosionPrefab;
    public bool ExplosionYesOrNo = false;
    public void ExplosionEn()
    {
        if (ExplosionYesOrNo)
        {
            var explosion = Instantiate(ExplosionPrefab);
            explosion.transform.position = transform.position;
        }
        return;
    }
}
