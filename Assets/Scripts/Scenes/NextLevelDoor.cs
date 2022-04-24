using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField] PlayerController player;
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
            canGoLoadNextLevel = player.playerHaveTheKey;
            isOnDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnDoor = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnDoor)
        {
            if (canGoLoadNextLevel)
            {
                audioSource.clip = openSound;
                audioSource.Play();
                StartCoroutine(levelManager.LoadNextLevel());
            }
            else
            {
                audioSource.clip = closeSound;
                audioSource.Play();
            }
        }
    }
}
