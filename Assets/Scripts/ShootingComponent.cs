using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private LaserProjectile LaserProjectilePrefab;
    [SerializeField] private Transform SpawnOrigin;

    void Update()
    {
        // TODO: implement the mobile controls
        if (Input.GetMouseButtonDown(0))
        {
            LaserProjectile lp = Instantiate(LaserProjectilePrefab, SpawnOrigin.position, Quaternion.identity);
            lp.SetDirection(SpawnOrigin.forward);
        }
    }


}
