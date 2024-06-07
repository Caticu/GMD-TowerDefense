using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    private AudioSource audioSource;
    private bool isPlaying = true;

    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

       
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("AudioSource component is missing on MusicPlayer GameObject.");
        }
    }

    private void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Audio started playing.");
        }
    }

    public void ToggleMusic()
    {
        if (audioSource != null)
        {
            if (isPlaying)
            {
                audioSource.Pause();
                isPlaying = false;
                Debug.Log("Audio paused.");
            }
            else
            {
                audioSource.UnPause();
                isPlaying = true;
                Debug.Log("Audio unpaused.");
            }
        }
        else
        {
            Debug.LogError("AudioSource component not found.");
        }
    }
}
