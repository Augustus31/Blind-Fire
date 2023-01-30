using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float explodeSpeed;
    public float explodeScale = 2;

    public float explodeRadius = 2;

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
        if(explodeLocation == null)
        {
            // pick a random position on screen to explode
            explodeLocation = new Vector3(Random.value * cwidth/2 - (cwidth/2), Random.value * cheight - (cheight / 2), this.transform.position.z);
        }
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
            //Start the fuze sound on spawn
            fuzeSource.Play();

            explodeSource = audioSources[1];
            explodeSource.clip = explodeSfx;
        }
        else
        {
            Debug.LogError("Bomb is missing audio sources");
        }
    }

    public void seek(Vector3 location)
    {
        explodeLocation = location;
        moveDirection = (explodeLocation - startingLocation).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Stop the sounds when time stopped (TODO: FIX CRAPPY SOLUTION) 
        if(Time.timeScale < 0.2)
        {
            fuzeSource.Stop();
        }
        Vector3 newMoveVector = moveDirection * moveSpeed * Time.deltaTime;
        if ((explodeLocation - this.transform.position).magnitude < newMoveVector.magnitude) {
            // Explosion -> turn off fuze sound, play explode sound
            if(tExplode == 0) {
                ObstacleAttributes[] obstacles = (ObstacleAttributes[]) GameObject.FindObjectsOfType(typeof(ObstacleAttributes));
                //Debug.Log(obstacles.Length + " obstacles");
                foreach (ObstacleAttributes obstacle in obstacles)
                {
                    if (obstacle.gameObject == this.gameObject) continue;
                    Vector3 obstaclePos = obstacle.gameObject.transform.position;
                    float dist = Vector3.Distance(obstaclePos, this.gameObject.transform.position);
                    //Debug.Log(dist + " far away from " + obstacle.name);
                    if (dist < explodeRadius)
                    {
                        Destroy(obstacle.gameObject);
                    }
                }

                fuzeSource.Stop();
                explodeSource.Play();
            }

            this.transform.position = explodeLocation;
            // Not sure if using scale is the best way to simulate explosion
            //Debug.Log("EXPLODE");
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
