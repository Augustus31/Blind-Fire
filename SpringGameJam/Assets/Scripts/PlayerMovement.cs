using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    // Jumping
    public float jumpForce;
    public int jumpsLeft;
    public bool grounded;

    // Momentum
    public float maxVel;
    public float minVel;
    public float velocity; //debug

    // Dashes
    public int dashesLeft;
    public bool LDashPrimer;
    public bool RDashPrimer;
    public bool dashMode;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Light/Dark")]
    public bool lightOn = true;
    //public Color backgroundCol;
    public Image background;
    public SpriteRenderer flashLight;
    public float toggleCooldown = 2f;
    public bool canToggle = true;
    //public TMPro cooldownText;

    private GameObject[] ghosts;
    private GameObject[] platforms;

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
        dashMode = false;

        // Getting all scene ghosts and platforms
        ghosts = GameObject.FindGameObjectsWithTag("Enemy");
        platforms = GameObject.FindGameObjectsWithTag("Platform");

        // Makes all ghost visible 
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<SpriteRenderer>().enabled = false;
        }
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

        // Dashing
        if (Input.GetMouseButtonDown(1))
        {
            if(dashesLeft > 0 && !grounded)
            {
                if (rb.velocity.x >= 0)
                {
                    StartCoroutine(dash(1));
                    dashesLeft = 0;
                }
                else if (rb.velocity.x < 0)
                {
                    StartCoroutine(dash(-1));
                    dashesLeft = 0;
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

            // Audio
            AudioManager.Instance.PlayAudio("Jump");
        }
        
    }

    public void ToggleLight()
    {
        if (canToggle)
        {
            // Remakes array in case some ghosts were shot
            ghosts = GameObject.FindGameObjectsWithTag("Enemy");

            // Turns off light
            if (lightOn)
            {
                lightOn = false;
                background.color = Color.black;
                flashLight.color = Color.white;

                // Makes all ghost visible and platforms invis
                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].GetComponent<SpriteRenderer>().enabled = true;
                }
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            // Turns on light
            else
            {
                lightOn = true;
                background.color = Color.white;
                flashLight.color = Color.black;

                // Makes all ghost invis and platforms visible
                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].GetComponent<SpriteRenderer>().enabled = false;
                }
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            // Audio
            AudioManager.Instance.PlayAudio("LightToggle");

            // Starts timer
            StartCoroutine(LightTimer());
        }

    }

    private IEnumerator LightTimer()
    {
        canToggle = false;
        yield return new WaitForSeconds(toggleCooldown);
        canToggle = true;
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
            if(col.gameObject.transform.position.y < gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().bounds.extents.y && Mathf.Abs(rb.velocity.y) < 0.05) //modify if box collider changes
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

    /*public IEnumerator ResetPrimer() 
    {
        yield return new WaitForSeconds(0.5f);
        LDashPrimer = false;
        RDashPrimer = false;
    }*/

    public IEnumerator dash(int a)
    {
        rb.velocity = new Vector2(25 * a, rb.velocity.y);
        dashMode = true;
        yield return new WaitForSeconds(0.3f);
        dashMode = false;
        jumpsLeft = 2;
    }


}