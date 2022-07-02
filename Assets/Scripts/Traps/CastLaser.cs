using System.Collections.Generic;
using UnityEngine;

public class CastLaser : MonoBehaviour
{
    [SerializeField] float defRayDistance = 100.0f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform laserStartPoint;

    [SerializeField] GameObject startVfx;
    [SerializeField] GameObject endVfx;

    [SerializeField] List<ParticleSystem> particles = new List<ParticleSystem>();

    [SerializeField] AudioSource audioSource;

    public bool laserActive = true;

    private void Start()
    {
        FillLists();
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
    }
    private void Update()
    {
        if (laserActive)
            ShootLaser();
    }

    private void ShootLaser()
    {
        lineRenderer.SetPosition(0, laserStartPoint.position);
        startVfx.transform.position = laserStartPoint.position;

        RaycastHit2D hit = Physics2D.Raycast(laserStartPoint.position, laserStartPoint.right);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("Player"))
            {
                PlayerController.Instance.playerLife.Die();
            }
        }
        else
        {
            lineRenderer.SetPosition(1, laserStartPoint.right * defRayDistance);
        }
        endVfx.transform.position = lineRenderer.GetPosition(1);
    }
    private void FillLists()
    {
        for (int i = 0; i< startVfx.transform.childCount; i++)
        {
            var ps = startVfx.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
        for (int i = 0; i< endVfx.transform.childCount; i++)
        {
            var ps = endVfx.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }
    public void DeactivateLineRenderer()
    {
        lineRenderer.enabled = false;
        laserActive = false;
        audioSource.Stop();
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }
    public void ActivateLineRenderer()
    {
        lineRenderer.enabled = true;
        laserActive = true;
        audioSource.Play();
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
    }
}

