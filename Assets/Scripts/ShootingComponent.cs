using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private GameObject LaserProjectilePrefab;
    [SerializeField] private Transform SpawnOrigin;

    void Update()
    {
        // TODO: implement the mobile controls
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(LaserProjectilePrefab, SpawnOrigin.position, Quaternion.Euler(90, 0, 0));
        }
    }


}
