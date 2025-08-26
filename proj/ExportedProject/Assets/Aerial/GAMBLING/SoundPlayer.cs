using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip boowompClip; // Assign this in the Inspector

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        // No need to load from Resources
    }

    public void PlaySound()
    {
        if (boowompClip != null)
        {
            audioSource.PlayOneShot(boowompClip);
        }
        else
        {
            Debug.LogWarning("boowompClip not assigned in Inspector!");
        }
    }
}
