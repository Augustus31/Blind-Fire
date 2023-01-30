using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public int lives;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    [SerializeField]
    public AudioClip hurtSfx;

    [HideInInspector]
    public AudioSource audioSource;

    private Animator damageIndicator;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        heart1 = GameObject.Find("heart1");
        heart2 = GameObject.Find("heart2");
        heart3 = GameObject.Find("heart3");
        damageIndicator = GameObject.Find("DamageIndicator").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.clip = hurtSfx;
        }else{
            Debug.LogError("Player is missing audio source component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            
            try
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            catch
            {
                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }

            // Play hurt sfx
            audioSource.Play();

            damageIndicator.SetTrigger("takeDamage");

            if (lives >= 1) {
                lives--;
            }

            updateHealthGraphic();
            if(lives <= 0)
            {
                // TODO: only trigger when life is depleted
                gameController.gameOver();
            }
        }
    }
    void updateHealthGraphic()
    {
        heart1.GetComponent<Image>().enabled = lives >= 3;
        heart2.GetComponent<Image>().enabled = lives >= 2;
        heart3.GetComponent<Image>().enabled = lives >= 1;

    }
    // Heal by 1, return true if sucessfuly increased hp
    public bool heal()
    {
        if (lives >= 3) return false;
        lives++;
        updateHealthGraphic();
        return true;
    }
}
