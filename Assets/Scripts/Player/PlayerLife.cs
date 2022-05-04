using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void Deneme()
    {
        GameManager.Instance.ReActivateBack();
        GameManager.Instance.SentPlayerBackToStart();
    }
}
