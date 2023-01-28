using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float explodeTime;
    public float speed;
    public float explodeRadius;

    private Vector3 explodeLocation;
    private Vector3 startingLocation;

    private float t = 0;

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
        this.transform.position = startingLocation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(startingLocation, explodeLocation, t);
        if (t < 1)
        {
            t += speed * Time.deltaTime;
        } else
        {
            Debug.Log("EXPLODE");
            GameObject.Destroy(this.gameObject);
        }
    }
}
