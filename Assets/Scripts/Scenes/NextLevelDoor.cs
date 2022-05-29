using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class NextLevelDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] PlayerActions playerActions;

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

    private void OnEnable()
    {
        playerActions.interactEvent += EnterDoor;
    }

    private void OnDisable()
    {
        playerActions.interactEvent -= EnterDoor;
    }

    private void EnterDoor()
    {
        if (isOnDoor)
        {
            if (canGoLoadNextLevel)
            {
                audioSource.clip = openSound;
                audioSource.Play();
                LevelManager.Instance.NextLevel();
            }
            else
            {
                audioSource.clip = closeSound;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canGoLoadNextLevel = keys.All(key => key.keyActivated == true);
            isOnDoor = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isOnDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnDoor = false;
    }
}
