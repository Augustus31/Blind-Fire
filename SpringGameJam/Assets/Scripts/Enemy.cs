using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    public float speed;
    public float playerDetectRange;

    [Header("Sound")]
    public float noiseRange = 1f;
    private bool playedSound = false;

    private GameObject player;

    [Header("Enemy Movement")]
    public float waveMagnitude;
    private float waveOffset;

    [Header("Particle")]
    public ParticleSystem deathParticles;

    void Start()
    {
        // Sets random pattern
        waveOffset = Random.Range(-waveMagnitude, waveMagnitude);

        player = GameObject.FindGameObjectWithTag("Player");
        playerDetectRange = 8.5f;
    }

    void Update()
    {
        // Move to player if in range
        float distance = (player.transform.position - this.transform.position).magnitude;
        if (distance <= playerDetectRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // Plays Close Audio Que
        if (distance <= noiseRange && !playedSound)
        {
            AudioManager.Instance.PlayAudio("GhostNear");
            playedSound = true;
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
            // Death Audio
            AudioManager.Instance.PlayAudio("GhostDie");

            // Particles
            Instantiate(deathParticles, transform.position, deathParticles.transform.rotation);

            // Die
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bullet Collision
        if (collision.gameObject.CompareTag("Bullet"))
        {
            /*
            if (collision.gameObject.GetComponent<Bullet>() != null)
            {
                collision.gameObject.GetComponent<Bullet>().BulletHit();
            }*/

            Destroy(collision.gameObject);

            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Restarts scene
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>().Restart();
        }
    }
}
