using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLaser : MonoBehaviour
{
    [SerializeField] float defRayDistance = 100.0f;
    [SerializeField] LineRenderer lnr;
    [SerializeField] Transform laserStartPoint;

    private void Update()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        lnr.SetPosition(0, laserStartPoint.position);

        RaycastHit2D hit = Physics2D.Raycast(laserStartPoint.transform.position, laserStartPoint.transform.right);
        if (hit)
        {
            lnr.SetPosition(1, hit.point);
        }
        else
        {
            lnr.SetPosition(1, laserStartPoint.right * defRayDistance);
        }
    }
}
