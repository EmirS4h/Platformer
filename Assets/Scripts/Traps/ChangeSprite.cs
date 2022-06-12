using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite newSprite;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spr.sprite = newSprite;
    }
}
