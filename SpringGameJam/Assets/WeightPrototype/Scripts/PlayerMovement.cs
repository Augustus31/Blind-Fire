using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float weight;

    private float ogWeight;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        moveInput.x = Input.GetAxisRaw("Horizontal") * speed;
        moveInput.y = rb.velocity.y;

        // Weight
        if (Input.GetKeyDown(KeyCode.E))
        {

        }

    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput;
    }
}
