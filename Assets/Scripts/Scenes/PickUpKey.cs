using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.Instance.playerHaveTheKey = true;
        gameObject.SetActive(false);
        GameManager.Instance.objects.Add(gameObject);
    }
}
