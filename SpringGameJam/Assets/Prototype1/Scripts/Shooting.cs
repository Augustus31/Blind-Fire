using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 mousePos)
    {
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
