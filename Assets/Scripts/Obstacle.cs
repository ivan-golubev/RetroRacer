using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private bool damageReceived;

    public void TakeLaserDamage()
    {
        if (damageReceived) { return; }
        damageReceived = true;
        Destroy(gameObject);
    }
}
