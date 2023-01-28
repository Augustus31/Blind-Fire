using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravity = 5;
    public float gravityRandomizer = 0;
    public float velocityMultiplier = 4;
    public float terminalVelocity = 10;

    public float velocityY = 0;

    // Start is called before the first frame update
    void Start()
    {
        float randomGravity = Random.value * gravityRandomizer - (gravityRandomizer/2);
        gravity += randomGravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPosition = this.transform.position;
        if (currentPosition.y > InputController.singleton.transform.position.y) {
            gravity = -Mathf.Abs(gravity);
        } else {
            gravity = Mathf.Abs(gravity);
        }
        // we multiply by Time.deltaTime in order make sure the game runs at variable frame rate.
        currentPosition.y += velocityMultiplier * velocityY * Time.deltaTime;
        this.transform.position = currentPosition;
        velocityY += gravity * Time.deltaTime;
        velocityY = Mathf.Clamp(velocityY, -terminalVelocity, terminalVelocity);
    }
}
