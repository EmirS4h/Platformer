using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTouch : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("move");
            Debug.Log("girdi");
        }
    }

}