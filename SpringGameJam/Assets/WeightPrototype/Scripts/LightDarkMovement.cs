using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDarkMovement : MonoBehaviour
{
    // Movement
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Light
    public bool lightOn = true;
    public SpriteRenderer background;

    // Shoot
    private Vector3 mousePos;
    public Transform shootPivot;


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

        // Mouse
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        shootPivot.eulerAngles = new Vector3(shootPivot.eulerAngles.x, shootPivot.eulerAngles.y, angle);

        // Light
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lightOn)
            {
                lightOn = false;
            }
            else
            {
                lightOn = true;
            }
        }

        // Sets background light
        if (lightOn)
        {
            background.color = Color.white;
        }
        else
        {
            background.color = Color.black;
        }

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput;
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
