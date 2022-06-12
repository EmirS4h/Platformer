using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class NextLevelDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] PlayerActions playerActions;

    [SerializeField] AudioClip openSound;

    public bool isOnDoor = false;

    private void Awake()
    {
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
            if (GameManager.Instance.totemsActivated)
            {
                audioSource.clip = openSound;
                audioSource.Play();
                UiManager.Instance.LevelEnd();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnDoor = true;
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
