using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    PlayerController playerController;
    AudioSource audioSource;
    
    [SerializeField] AudioClip keySound;
    
    [Header("Runing Sounds")]
    [SerializeField] AudioClip[] grassSounds;
    [SerializeField] AudioClip[] rockSounds;
    [SerializeField] AudioClip[] woodSounds;
    
    [Header("Jump Start Sounds")]
    [SerializeField] AudioClip[] grassJumpSounds;
    [SerializeField] AudioClip[] rockJumpSounds;
    [SerializeField] AudioClip[] woodJumpSounds;

    [Header("Jump Landing Sounds")]
    [SerializeField] AudioClip[] grassLandingSounds;
    [SerializeField] AudioClip[] rockLandingSounds;
    [SerializeField] AudioClip[] woodLandingSounds;

    public int groundType = 0;
    public bool playLandingSound = false;
    int rnd;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            audioSource.PlayOneShot(keySound);
        }
        if (playLandingSound)
        {
            PlayLandingSound();
        }
    }

    public void PlayFootSteps()
    {
        rnd = Random.Range(0, 2);
        switch (groundType)
        {
            case 0:
                audioSource.PlayOneShot(grassSounds[rnd]);
                break;
            case 1:
                audioSource.PlayOneShot(rockSounds[rnd]);
                break;
            case 2:
                audioSource.PlayOneShot(woodSounds[rnd]);
                break;
        }
    }
    public void PlayJumpSound()
    {
        rnd = Random.Range(0, 2);
        switch (groundType)
        {
            case 0:
                audioSource.PlayOneShot(grassJumpSounds[rnd]);
                break;
            case 1:
                audioSource.PlayOneShot(rockJumpSounds[rnd]);
                break;
            case 2:
                audioSource.PlayOneShot(woodJumpSounds[rnd]);
                break;
        }
    }

    public void PlayLandingSound()
    {
        rnd = Random.Range(0, 2);
        switch (groundType)
        {
            case 0:
                audioSource.PlayOneShot(grassLandingSounds[rnd]);
                break;
            case 1:
                audioSource.PlayOneShot(rockLandingSounds[rnd]);
                break;
            case 2:
                audioSource.PlayOneShot(woodLandingSounds[rnd]);
                break;
        }
    }
}
