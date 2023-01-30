using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 0;

    private float cheight;
    private float cwidth;

    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cheight = 2f * mainCamera.orthographicSize;
        cwidth = cheight * mainCamera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        if (transform.position.x < -(cwidth+2)) {
            GameObject.Destroy(this.gameObject);
        }
    }
}
