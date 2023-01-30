using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public PlayerController playerController;
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
            }
        }
    }
}
