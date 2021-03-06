using Cinemachine;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private Transform playerTransform;
    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        vcam.Follow = playerTransform;
    }
}
