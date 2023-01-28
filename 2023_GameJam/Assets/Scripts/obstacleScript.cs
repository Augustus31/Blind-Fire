using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour
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
        camPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1f * movespeed * Time.deltaTime, 0, 0));
        if(transform.position.x < camPos.x - 1.5 * cwidth)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
