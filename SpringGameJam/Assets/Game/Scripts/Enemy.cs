using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    public float speed;
    public float playerDetectRange;

    private GameObject player;

    [Header("Enemy Movement")]
    [SerializeField]
    private float waveOffset;

    void Start()
    {
        // Sets random pattern
        waveOffset = Random.Range(-3.14f,3.14f);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Move to player if in range
        float distance = (player.transform.position - this.transform.position).magnitude;
        if (distance <= playerDetectRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // Adds wave
        transform.position += (Vector3.up * Mathf.Sin((5*Time.time) + waveOffset)) * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        // Die
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bullet Collision
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>() != null)
            {
                collision.gameObject.GetComponent<Bullet>().BulletHit();
            }

            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Restarts scene
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>().Restart();
        }
    }
}
