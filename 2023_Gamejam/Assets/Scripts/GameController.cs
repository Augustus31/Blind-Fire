using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject testObstacle;
    public List<ObstacleAttributes> obstacles;

    public GameObject pickupObject;

    public float score;
    public float startTime;
    public bool gameActive;
    public float cheight;
    public float cwidth;
    public Vector3 camPos;
    public float spawnClock = 1;
    public float spawnOffset = 2;

    public UIController uiController;
    public InputController inputController;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
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
        StartCoroutine(spawnPickups());
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
            float x = score;
            // change these formulas to balance difficulty
            float newSpawnTime = (spawnTime / 2) / (0.5f * x + 1) + (spawnTime / 2);
            float newSpawnChance = spawnChance * (x / (x + 1)) + spawnChance;
            Debug.Log(newSpawnTime);

            float randomIfSpawn = Random.value;
            float randomY = Random.value * cheight - (cheight / 2);
            if (randomIfSpawn < newSpawnChance)
            {
                Instantiate(obstacle, new Vector3(camPos.x + cwidth / 2 + spawnOffset, randomY, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(newSpawnTime + (Random.value * 0.5f - 0.25f));
        }
    }

    public IEnumerator spawnPickups()
    {
        while (gameActive)
        {
            Debug.Log("Try spawn pickup");
            int health = playerController.lives;
            float spawnHealOdds = 0;
            if(health == 1)
            {
                spawnHealOdds = 0.15f;
            }else if(health == 2)
            {
                spawnHealOdds = 0.09f;
            }

            int numObstacles = GameObject.FindGameObjectsWithTag("obstacle").Length;
            float spawnNukeOdds = (1f - 1f / (numObstacles * 0.2f + 1f)) * 0.2f;

            bool spawnHeal = Random.value < spawnHealOdds; 
            bool spawnNuke = Random.value < spawnNukeOdds;
            bool spawnPickup = spawnHeal || spawnNuke;
            Pickup.PickupType pickupType = Pickup.PickupType.Health;
            if (spawnHeal)
            {
                pickupType = Pickup.PickupType.Health;
            }
            else if (spawnNuke)
            {
                pickupType = Pickup.PickupType.Nuke;
            }

            Debug.Log("Spawning?" + spawnHealOdds + " " + spawnNukeOdds);
            if (spawnPickup)
            {
                Debug.Log("Spawning " + pickupType);
                GameObject pickup = Instantiate(pickupObject);
                pickup.GetComponent<Pickup>()?.setType(pickupType); 
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void gameOver()
    {
        gameActive = false;
        Time.timeScale = 0;
        inputController.disabled = true;
        uiController.gameOver();
        Cursor.visible = true;
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void quitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
