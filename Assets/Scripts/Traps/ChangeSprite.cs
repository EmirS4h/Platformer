using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite newSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spr.sprite = newSprite;
    }
}