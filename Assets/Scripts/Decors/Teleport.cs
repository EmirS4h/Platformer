using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Teleport : MonoBehaviour
{
    [SerializeField] Transform whereTo;
    public bool canTeleport = false;
    public bool teleporting = false;
    public GameObject willBeTeleported;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            willBeTeleported = collision.gameObject;
            canTeleport = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        willBeTeleported = null;
        canTeleport = false;
    }
    // Update is called once per frame
    //void Update()
    //{
    //    // Input.GetKeyDown(KeyCode.E)
    //    // CrossPlatformInputManager.GetButtonDown("InteractBtn")
    //    if (CrossPlatformInputManager.GetButtonDown("InteractBtn") && canTeleport && !teleporting)
    //    {
    //        StartCoroutine(StartTeleportation());
    //    }
    //}

    public IEnumerator StartTeleportation()
    {
        teleporting = true;
        yield return new WaitForSeconds(3);
        if (willBeTeleported != null)
        {
            willBeTeleported.transform.position = new Vector2(whereTo.transform.position.x, whereTo.transform.position.y);
        }
        teleporting = false;
    }
}
