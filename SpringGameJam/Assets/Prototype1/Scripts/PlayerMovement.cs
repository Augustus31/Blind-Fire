using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Light/Dark")]
    public bool lightOn = true;
    public SpriteRenderer background;

    [Header("Shooting")]
    public Transform shootPivot;
    private Vector3 mousePos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Physics horizontal movement
        rb.velocity = moveInput;
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
        }
        // Turns on light
        else
        {
            lightOn = true;
            background.color = Color.white;
        }
    }


    public void Shoot() {
        Collider2D[] cols = Physics2D.OverlapPointAll(mousePos);
        if (cols.Length != 0)
        {
            foreach (Collider2D col in cols)
            {
                Enemy enemyScript = col.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(1);
                }
            }
        }
    }
}
