using UnityEngine;

public class TeleportStone : MonoBehaviour
{
    [SerializeField] bool playerOnTp = false;
    [SerializeField] PlayerActions playerActions;

    private void OnEnable()
    {
        playerActions.interactEvent += HandleInteraction;
    }

    private void OnDisable()
    {
        playerActions.interactEvent -= HandleInteraction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnTp = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnTp = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnTp = false;
    }
    private void HandleInteraction()
    {
        if (playerOnTp)
        {
            PlayerController.Instance.tpStonePlaced = false;
            Destroy(gameObject);
        }
    }
}
