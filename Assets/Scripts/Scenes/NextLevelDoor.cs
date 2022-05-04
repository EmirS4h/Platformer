using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

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
            PlayerController.Instance.isGameOver = true;
            if (canGoLoadNextLevel)
            {
                audioSource.clip = openSound;
                audioSource.Play();
                levelManager.LevelEndingScreen();
                //StartCoroutine(levelManager.LoadNextLevel());
            }
            else
            {
                audioSource.clip = closeSound;
                audioSource.Play();
            }
        }
    }
}
