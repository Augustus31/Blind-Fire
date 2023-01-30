using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null)
        {
            Debug.LogError("Missing audio source");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void playHealSfx()
    {
        audioSource.Play(); 
    }
}
