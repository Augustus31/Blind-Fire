using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    private GameObject player;
    private float waveOffset;


    // Start is called before the first frame update
    void Start()
    {
        waveOffset = Random.Range(-3.14f,3.14f);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Move to player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.position += (Vector3.up * Mathf.Sin((5*Time.time) + waveOffset)) * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("OW");
        health -= damage;

        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>() != null)
            {
                collision.gameObject.GetComponent<Bullet>().BulletHit();
            }

            TakeDamage(1);
        }
    }
}
