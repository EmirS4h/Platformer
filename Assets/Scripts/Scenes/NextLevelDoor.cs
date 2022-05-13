using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class NextLevelDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;

    public bool canGoLoadNextLevel = false;
    public bool isOnDoor = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canGoLoadNextLevel = PlayerController.Instance.playerHaveTheKey;
            isOnDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnDoor = false;
    }

    private void Update()
    {
        // CrossPlatformInputManager.GetButtonDown("InteractBtn")
        // Input.GetKeyDown(KeyCode.E)
        if (Input.GetKeyDown(KeyCode.E) && isOnDoor)
        {
            if (canGoLoadNextLevel)
            {
                PlayerController.Instance.isGameOver = true;
                audioSource.clip = openSound;
                audioSource.Play();
                LevelManager.Instance.LevelEndingScreen();
            }
            else
            {
                audioSource.clip = closeSound;
                audioSource.Play();
            }
        }
    }
}
