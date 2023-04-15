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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector3 mousePos)
    {
        if (canShoot)
        {
            // Instantiates bullet object
            Bullet bullet = Instantiate(bulletObj, bulletSpawn.position, bulletSpawn.rotation).GetComponent<Bullet>();
            bullet.SetBulletSpeed(bulletSpeed);



            // Starts timer
            StartCoroutine(ShootTimer());

        }

        // Send a projectile from gun shoot pos to enemy
        // if proj hits enemy, enemy dies
    }

    private IEnumerator ShootTimer()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletCooldown);
        canShoot = true;
    }


}
