using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody2D rb;
    [SerializeField] AudioClip keySound;
    
    [Header("Running Sounds")]
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
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            audioSource.PlayOneShot(keySound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playLandingSound && rb.velocity.y < 10.0f)
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
