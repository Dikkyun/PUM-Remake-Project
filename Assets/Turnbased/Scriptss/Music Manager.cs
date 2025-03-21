using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
