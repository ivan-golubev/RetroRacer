using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool damageReceived;
    private ObstaclePart[] parts;

    void Start()
    {
        parts = GetComponentsInChildren<ObstaclePart>();
    }

    public void TakeLaserDamage(ContactPoint cPoint)
    {
        if (damageReceived) { return; }
        damageReceived = true;
        foreach (var p in parts)
        {
            p.TheRigidBody.useGravity = true;
        }
        Destroy(gameObject, 3.0f);
    }
}
