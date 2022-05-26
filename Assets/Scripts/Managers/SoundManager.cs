using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;
    [SerializeField] AudioClip keySound;

    [Header("Jump Start Sounds")]
    [SerializeField] AudioClip rockJumpSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            audioSource.PlayOneShot(keySound);
        }
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(rockJumpSound);
    }
    public void PlaySoundOneShot(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}
