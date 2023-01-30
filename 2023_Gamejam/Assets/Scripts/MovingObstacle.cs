using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float movespeed;
    public float cheight;
    public float cwidth;
    public Vector3 camPos;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 2.0f;
        Camera cam = Camera.main;
        cheight = 2f * cam.orthographicSize;
        cwidth = cheight * cam.aspect;
        camPos = cam.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(new Vector3(-1f * movespeed * Time.deltaTime, 0, 0));
        transform.position = new Vector3(this.transform.position.x - movespeed * Time.deltaTime, this.transform.position.y, 0);
        if(transform.position.x < camPos.x - 1.5 * cwidth)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
