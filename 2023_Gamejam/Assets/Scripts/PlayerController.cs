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

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        heart1 = GameObject.Find("heart1");
        heart2 = GameObject.Find("heart2");
        heart3 = GameObject.Find("heart3");
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

            if (lives > 1)
            {
                if(lives == 3)
                {
                    heart1.GetComponent<Image>().enabled = false;
                }
                else if (lives == 2)
                {
                    heart2.GetComponent<Image>().enabled = false;
                }
                lives--;
            }
            else
            {
                heart3.GetComponent<Image>().enabled = false;
                lives = 0;
                Debug.Log("Collision detected!");
                // TODO: only trigger when life is depleted
                gameController.gameOver();
            }  
        }
    }
}
