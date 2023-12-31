using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextlevelTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Audio
            AudioManager.Instance.PlayAudio("LevelComplete");

            // Loads Next Scene
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Menu>().NextLevel();
        }
    }
}
