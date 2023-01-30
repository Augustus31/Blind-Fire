using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public float timeBetweenSpawn = 1;
    public GameObject itemToSpawn;

    private bool isSpawning = false;

    private float cheight;
    private float cwidth;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cheight = 2f * mainCamera.orthographicSize;
        cwidth = cheight * mainCamera.aspect;
        isSpawning = true;
        StartCoroutine("CloudSpawner");
    }

    IEnumerator CloudSpawner() {
        while (isSpawning) {
            Instantiate(itemToSpawn, new Vector3(cwidth+2, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
