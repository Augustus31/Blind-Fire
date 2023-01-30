using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pickup : MonoBehaviour
{
    public enum PickupType
    {
        Health,
        Nuke
    }

    public PickupType pickupType;


    public Sprite healthSprite;
    public Sprite nukeSprite;

    public bool pickedUp = false;
    private bool leaving = false;
    private IEnumerator leaveRoutine;


    public float moveSpeed = 0.4f;
    public float secondsUntillLeave = 6;



    private Animator animator;

    private Vector3 targetLocation;
    private Vector3 startingLocation;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SpriteRenderer spriteR = GetComponent<SpriteRenderer>();
        switch (pickupType)
        {
            case PickupType.Health:
                spriteR.sprite = healthSprite;
                break;
            case PickupType.Nuke:
                spriteR.sprite = nukeSprite;
                break;
        }


        targetLocation = getTargetLoc();
        // set spawn location
        startingLocation = getStartingLoc();
        moveDirection = (targetLocation - startingLocation).normalized;
        this.transform.position = startingLocation;
    }
    Vector3 getTargetLoc()
    {
        Camera cam = Camera.main;
        float cheight = 2f * cam.orthographicSize;
        float cwidth = cheight * cam.aspect;
        return new Vector3(Mathf.Lerp(cwidth*0.3f,cwidth * 0.5f,Random.value), Random.value* cheight -(cheight / 2), this.transform.position.z);
    }
    Vector3 getStartingLoc()
    {
        Camera cam = Camera.main;
        float cheight = 2f * cam.orthographicSize;
        float cwidth = cheight * cam.aspect;
        return new Vector3(cwidth / 2 + 2, Random.value * cheight - (cheight / 2), this.transform.position.z);
    }
    public IEnumerator waitToLeave()
    {
        yield return new WaitForSeconds(secondsUntillLeave);
        Debug.Log("Leaving!");
        leaving = true;
        // go to right
        startingLocation = targetLocation;
        targetLocation = getStartingLoc();
        moveDirection = (targetLocation - startingLocation).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newMoveVector = moveDirection * moveSpeed * Time.deltaTime;
        if ((targetLocation - this.transform.position).magnitude < newMoveVector.magnitude)
        {
            this.transform.position = targetLocation;
            if (leaving)
            {
                pickup();
            }
            else
            {
                if (leaveRoutine == null)
                {
                    leaveRoutine = waitToLeave();
                    StartCoroutine(leaveRoutine);
                }
                //go elsewhere
                startingLocation = targetLocation;
                targetLocation = getTargetLoc();
                moveDirection = (targetLocation - startingLocation).normalized;
            }
        }
        else
        {
            this.transform.position += newMoveVector;
        }
    }

    public void pickup()
    {
        animator.SetTrigger("shrink");
        Object.Destroy(gameObject,1.5f);
    }
}
