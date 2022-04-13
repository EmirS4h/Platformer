using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    PlayerController playerController;
    AudioSource audioSource;
    [SerializeField] AudioClip keySound;
    [SerializeField] AudioClip[] grassSounds;
    [SerializeField] AudioClip[] rockSounds;
    [SerializeField] AudioClip[] woodSounds;
    [SerializeField] AudioClip[] jumpSounds;
    [SerializeField] AudioClip[] landingSounds;
    public int groundType = 0;
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
        audioSource.PlayOneShot(jumpSounds[rnd]);
    }

    public void PlayLandingSound()
    {
        if (!playerController.isGrounded)
        {
            rnd = Random.Range(0, 2);
            audioSource.PlayOneShot(landingSounds[rnd]);
        }
    }
}
