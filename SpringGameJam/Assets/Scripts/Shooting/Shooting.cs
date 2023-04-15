using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    public float bulletSpeed;
    public float bulletCooldown;
    public bool canShoot = true;

    public Transform bulletSpawn;
    public GameObject bulletObj;

    public void Shoot()
    {
        if (canShoot)
        {
            // Instantiates bullet object
            Bullet bullet = Instantiate(bulletObj, bulletSpawn.position, bulletSpawn.rotation).GetComponent<Bullet>();
            bullet.SetBulletSpeed(bulletSpeed);

            // Starts timer
            StartCoroutine(ShootTimer());
        }
    }

    private IEnumerator ShootTimer()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletCooldown);
        canShoot = true;
    }


}
