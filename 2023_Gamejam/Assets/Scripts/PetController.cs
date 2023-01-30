using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject bombObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // add collision
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "pickup")
        {
            Pickup pickup = collision.gameObject.GetComponent<Pickup>();
            if(pickup != null && pickup.pickedUp == false)
            {
                pickup.pickedUp = true;
                pickup.pickup();
                if(pickup.pickupType == Pickup.PickupType.Health) { 
                    playerController.heal();
                }
                if (pickup.pickupType == Pickup.PickupType.Nuke)
                {
                    GameObject bomb = Instantiate(bombObject);
                    BombController bc = bomb.GetComponent<BombController>();
                    //bc.seek(Vector3.zero);
                    bc.explodeRadius = 100;
                    bc.explodeScale = 15;
                    try
                    {
                        bomb.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    catch
                    {
                        bomb.GetComponent<CircleCollider2D>().enabled = false;
                    }
                    bc.explodeLocation = Vector3.zero;
                    bomb.transform.position = bc.explodeLocation;
                }
            }
        }
    }
}
