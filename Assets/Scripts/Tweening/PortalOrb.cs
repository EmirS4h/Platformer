using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOrb : MonoBehaviour
{
    [SerializeField] Transform otherLocation;
    [SerializeField] float travelDuration = 1.0f;
    [SerializeField] float loopDuration = 1.0f;
    [SerializeField] Tween tween;

    [SerializeField] TrailRenderer tr;

    private void Awake()
    {
        tr = GetComponent<TrailRenderer>();
        otherLocation = FindObjectOfType<NextLevelDoor>().transform;
    }
    private void Start()
    {
        GameManager.Instance.portalOrb = gameObject;
        tween.MoveToLocation(gameObject, otherLocation.position + new Vector3(0.0f, 2.0f, 0.0f), travelDuration);
        StartCoroutine(MoveUpDown());
    }
    private IEnumerator MoveUpDown()
    {
        yield return new WaitForSeconds(travelDuration + 0.5f);
        tween.MoveUpDown(gameObject, gameObject.transform.position.y + 1.0f, loopDuration);
        tr.enabled = false;
    }
}
