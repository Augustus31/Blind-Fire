using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public bool grounded;
    public float maxVel;
    public float minVel;
    public float velocity; //debug

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Light/Dark")]
    public bool lightOn = true;
    public SpriteRenderer background;
    public SpriteRenderer flashLight;

    [Header("Shooting")]
    public Transform shootPivot;
    private Vector3 mousePos;
    private Shooting shootScript;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootScript = GetComponent<Shooting>();
        grounded = true;
        minVel = -7;
        maxVel = 7;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.x;

        // Getting Movement & Input
        Move();

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Light
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
        }

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            shootScript.Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Physics horizontal movement
        if (grounded)
        {
            rb.velocity = moveInput;
        }
        else
        {
            rb.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * speed / 15, 0);
            if (rb.velocity.x < minVel)
            {
                rb.velocity = new Vector2(minVel, rb.velocity.y);
            }
            if (rb.velocity.x > maxVel)
            {
                rb.velocity = new Vector2(maxVel, rb.velocity.y);
            }
        }

    }

    public void Move()
    {
        // Horizontal Movement Input
        moveInput.x = Input.GetAxisRaw("Horizontal") * speed;
        moveInput.y = rb.velocity.y;

        // Mouse Input
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Rotates Around player
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        shootPivot.eulerAngles = new Vector3(shootPivot.eulerAngles.x, shootPivot.eulerAngles.y, angle);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void ToggleLight()
    {
        // Turns off light
        if (lightOn)
        {
            lightOn = false;
            background.color = Color.black;
            flashLight.color = Color.white;
        }
        // Turns on light
        else
        {
            lightOn = true;
            background.color = Color.white;
            flashLight.color = Color.black;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "platform")
        {
            grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        grounded = false;
    }
}