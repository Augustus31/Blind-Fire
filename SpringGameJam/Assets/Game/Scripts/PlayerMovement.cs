using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public bool grounded;
    public float maxVel;
    public float minVel;
    public int jumpsLeft;
    public int dashesLeft;
    public bool LDashPrimer;
    public bool RDashPrimer;
    public bool dashMode;
    public float velocity; //debug

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Light/Dark")]
    public bool lightOn = true;
    public Color backgroundCol;
    public Image background;
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
        jumpsLeft = 2;
        dashesLeft = 1;
        LDashPrimer = false;
        RDashPrimer = false;
        dashMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.x;

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
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

        //dashing
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(dashesLeft > 0 && !grounded)
            {
                if (!LDashPrimer)
                {
                    LDashPrimer = true;
                    StartCoroutine(ResetPrimer());
                }
                else
                {
                    StartCoroutine(dash(-1));
                    LDashPrimer = false;
                    dashesLeft = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (dashesLeft > 0)
            {
                if (!RDashPrimer)
                {
                    RDashPrimer = true;
                    StartCoroutine(ResetPrimer());
                }
                else
                {
                    StartCoroutine(dash(1));
                    RDashPrimer = false;
                    dashesLeft--;
                }
            }
        }

        // Getting Movement & Input
        Move();
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
            if (!dashMode)
            {
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
        if(jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpsLeft--;
        }
        
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
            background.color = backgroundCol;
            flashLight.color = Color.black;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            grounded = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            if(col.gameObject.transform.position.y < gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y) //modify if box collider changes
            {
                jumpsLeft = 2;
                dashesLeft = 1;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        grounded = false;
    }

    public IEnumerator ResetPrimer() 
    {
        yield return new WaitForSeconds(0.5f);
        LDashPrimer = false;
        RDashPrimer = false;
    }

    public IEnumerator dash(int a)
    {
        rb.velocity = new Vector2(25 * a, rb.velocity.y);
        dashMode = true;
        yield return new WaitForSeconds(0.3f);
        dashMode = false;
        jumpsLeft = 2;
    }


}