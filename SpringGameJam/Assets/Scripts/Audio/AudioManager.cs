using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] audioSources;
    public bool startMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            // Keeps alive
            DontDestroyOnLoad(this.gameObject);
        }

        foreach (Sound s in audioSources)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        startMusic = false;
        if (!startMusic)
        {
            SceneManager.LoadScene("Music", LoadSceneMode.Additive);
            startMusic = true;
        }
    }
    
    public void PlayAudio(string name)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].clipName.Equals(name))
            {
                audioSources[i].source.Play();
            }
        }
    }

    public void StopAllAudio()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].source.Stop();
        }
    }
}
