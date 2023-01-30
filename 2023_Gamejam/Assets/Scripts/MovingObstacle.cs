using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float movespeed;
    public float cheight;
    public float cwidth;
    public Vector3 camPos;
    public bool scoreContribution;
    public GameController controllerScript;

    // Start is called before the first frame update
    void Start()
    {       
        Camera cam = Camera.main;
        cheight = 2f * cam.orthographicSize;
        cwidth = cheight * cam.aspect;
        camPos = cam.transform.position;
        scoreContribution = false;
        controllerScript = GameObject.Find("GameController").GetComponent<GameController>();
        movespeed = 1.5f * Random.Range(0.5f, 1.5f) * (2 * Mathf.Log10(controllerScript.score + 1) + 1 + controllerScript.score / 20f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(-1f * movespeed * Time.deltaTime, 0, 0));
        transform.position = new Vector3(this.transform.position.x - movespeed * Time.deltaTime, this.transform.position.y, 0);
        if(transform.position.x < camPos.x - 1.5 * cwidth)
        {
            GameObject.Destroy(this.gameObject);
        }
        if((transform.position.x < GameObject.Find("Player").transform.position.x - 0.2) && !scoreContribution)
        {
            scoreContribution = true;
            controllerScript.score += 1;
            //Debug.Log("score raised");

        }
    }
}
