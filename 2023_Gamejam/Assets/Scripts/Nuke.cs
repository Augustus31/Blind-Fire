using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    private GameController controllerScript;
    private Animator anim;
    
    private void Start() {
        controllerScript = GameObject.Find("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();
    }

    public void Explode() {
        StartCoroutine("ExplodeTime");
    }

    IEnumerator ExplodeTime() {
        // yeaaah babyyy, all obstacles gone!!!
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Explode");
        GetComponent<AudioSource>().Play();
        foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("obstacle")) {
            GameObject.Destroy(obstacle);
            controllerScript.score += 1;
        }
        yield return new WaitForSeconds(0.25f);
        GameObject.Destroy(this.gameObject);
    }
}
