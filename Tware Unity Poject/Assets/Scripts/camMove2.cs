using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove2 : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && rb.velocity.z < 15)
        {
            rb.AddForce(0, 0, 1, ForceMode.Impulse);
        }
    }
}
