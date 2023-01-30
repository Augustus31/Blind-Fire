using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float explodeSpeed;
    public float explodeScale = 2;

    public float fuzeVolume = 0.5f;
    public AudioClip fuzeSfx;
    public AudioClip explodeSfx;


    private Vector3 explodeLocation;
    private Vector3 startingLocation;
    private Vector3 moveDirection;

    private float tExplode = 0;

    private Animator bombAnimator;

    private AudioSource fuzeSource;
    private AudioSource explodeSource; 

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        float cheight = 2f * cam.orthographicSize;
        float cwidth = cheight * cam.aspect;
        // pick a random position on screen to explode
        explodeLocation = new Vector3(Random.value * cwidth/2 - (cwidth/2), Random.value * cheight - (cheight / 2), this.transform.position.z);
        // set spawn location
        startingLocation = new Vector3(cwidth / 2 + 2, Random.value * cheight - (cheight / 2), this.transform.position.z);
        moveDirection = (explodeLocation - startingLocation).normalized;
        this.transform.position = startingLocation;

        bombAnimator = GetComponent<Animator>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        if(audioSources.Length >= 2)
        {
            fuzeSource = audioSources[0];
            fuzeSource.loop = true;
            fuzeSource.clip = fuzeSfx;
            fuzeSource.volume = fuzeVolume;

            explodeSource = audioSources[1];
            explodeSource.clip = explodeSfx;
        }
        else
        {
            Debug.LogError("Bomb is missing audio sources");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(fuzeSource != null && !fuzeSource.isPlaying)
        {
            //Start the fuze sound on spawn
            fuzeSource.Play();
        }
        Vector3 newMoveVector = moveDirection * moveSpeed * Time.deltaTime;
        if ((explodeLocation - this.transform.position).magnitude < newMoveVector.magnitude) {
            // Explosion -> turn off fuze sound, play explode sound
            if(tExplode == 0) { 
                fuzeSource.Stop();
                explodeSource.Play();
            }

            this.transform.position = explodeLocation;
            // Not sure if using scale is the best way to simulate explosion
            Debug.Log("EXPLODE");
            bombAnimator.SetTrigger("explode");
            float scale = Mathf.Lerp(1, explodeScale, tExplode);
            tExplode += explodeSpeed * Time.deltaTime;
            this.transform.localScale = new Vector3(scale, scale, scale);
            // Destroys the bomb when it finish expanding
            if (tExplode > 1)
            {
                GameObject.Destroy(this.gameObject);
            }
        } else
        {
            this.transform.position += newMoveVector;
        }
    }
}
