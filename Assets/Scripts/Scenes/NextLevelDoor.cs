using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] LevelManager levelManager;

    AudioSource audioSource;

    public bool canGoLoadNextLevel = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canGoLoadNextLevel = player.playerHaveTheKey;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canGoLoadNextLevel)
        {
            audioSource.Play();
            StartCoroutine(levelManager.LoadNextLevel());
        }
    }
}
