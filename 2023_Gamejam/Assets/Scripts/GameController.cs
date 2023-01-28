using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject testObstacle;
    public List<ObstacleAttributes> obstacles;
    public float score;
    public float startTime;
    public bool gameActive;
    public float cheight;
    public float cwidth;
    public Vector3 camPos;
    public float spawnClock = 1;
    public float spawnOffset = 2;

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
        foreach (ObstacleAttributes obstacle in obstacles)
        {
            StartCoroutine(spawnObstacles(obstacle.gameObject, obstacle.timeToSpawn, obstacle.chanceToSpawn));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //add some score function based on time passed
        //update obstacle spawn clock to be shorter as time goes on


    }

    public IEnumerator spawnObstacles(GameObject obstacle, float spawnTime, float spawnChance)
    {
        while (gameActive)
        {
            float randomIfSpawn = Random.value;
            float randomY = Random.value * cheight - (cheight / 2);
            if (randomIfSpawn < spawnChance)
            {

                Instantiate(obstacle, new Vector3(camPos.x + cwidth / 2 + spawnOffset, randomY, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnTime + (Random.value * 0.5f - 0.25f));
        }
    }
}
