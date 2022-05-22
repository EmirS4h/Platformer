using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;
public class NextLevelDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;

    public bool canGoLoadNextLevel = false;
    public bool isOnDoor = false;

    [SerializeField] LevelKey[] keys;

    private void Awake()
    {
        keys = FindObjectsOfType<LevelKey>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canGoLoadNextLevel = keys.All(key => key.keyActivated == true);
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
