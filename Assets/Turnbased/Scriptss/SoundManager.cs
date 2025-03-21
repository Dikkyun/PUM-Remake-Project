using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip argumentSound;
    public AudioClip silenceSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip backgroundMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play(); // Start playing background music
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "attack":
                audioSource.PlayOneShot(attackSound);
                break;
            case "argument":
                audioSource.PlayOneShot(argumentSound);
                break;
            case "silence":
                audioSource.PlayOneShot(silenceSound);
                break;
            case "win":
                audioSource.PlayOneShot(winSound);
                break;
            case "lose":
                audioSource.PlayOneShot(loseSound);
                break;
        }
    }
}

