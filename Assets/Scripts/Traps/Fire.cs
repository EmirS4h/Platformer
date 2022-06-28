using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;

    private void Start()
    {
        InvokeRepeating(nameof(FireBullet), 1.0f, 1.0f);
    }

    private void FireBullet()
    {
        Instantiate(bullet, bulletPos.position, bulletPos.transform.rotation);
    }
}
