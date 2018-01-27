using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour {
    public GameObject projectile, shootSpawn;
    public float fireRate;

    private float fireCooldown;
    
    void Start () {
        fireCooldown = Time.time + fireRate;
    }

    void FixedUpdate () {
        if (fireCooldown < Time.time)
        {
            fireCooldown = Time.time + fireRate;
            Instantiate(projectile, shootSpawn.transform.position, shootSpawn.transform.rotation);
        }

    }
}
