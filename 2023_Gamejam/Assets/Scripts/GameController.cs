using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject testObstacle;
    public float score;
    public float startTime;
    public bool gameActive;
    public float cheight;
    public float cwidth;
    public Vector3 camPos;
    public float spawnClock;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        cheight = 2f * cam.orthographicSize;
        cwidth = cheight * cam.aspect;
        camPos = Camera.main.transform.position;
        testObstacle = (GameObject)Resources.Load("exampleObstacle");
        score = 0;
        startTime = Time.time;
        gameActive = true;
        spawnClock = 1;
        StartCoroutine(spawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {
        //add some score function based on time passed
        //update obstacle spawn clock to be shorter as time goes on

        
    }

    public IEnumerator spawnObstacles()
    {
        while (gameActive)
        {
            float random = Random.value;
            if (random > 0.5f)
            {
                //this is very basic, obviously we will need a y-component to the random spawn
                Instantiate(testObstacle, new Vector3(camPos.x + cwidth, 0, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnClock);
        }
    }
}
