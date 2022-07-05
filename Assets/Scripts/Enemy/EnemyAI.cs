using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] SpriteRenderer spr;
    [SerializeField] BoxCollider2D bxc;

    [SerializeField] Vector3 startingPos;
    [SerializeField] Vector3 roamPos;
    [SerializeField] float reachedPositionDistance = 1.0f;
    [SerializeField] Transform checkPlayer;
    [SerializeField] float playerCheckDistance;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool playerFound = false;

    [SerializeField] Transform checkGround;
    [SerializeField] Transform checkEdge;
    [SerializeField] bool edgeFound = false;
    [SerializeField] float checkGroundRadius;
    [SerializeField] float edgeCheckDistance = 0.4f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded = true;

    [SerializeField] float speed = 10.0f;
    [SerializeField] float roamSpeed = 5.0f;

    [SerializeField] bool isFacingRight = true;

    [SerializeField] int health = 100;

    private enum State
    {
        Roam,
        Follow,
        GoBackToStart,
        Moving,
    }
    [SerializeField] State state;

    private void Awake()
    {
        startingPos = transform.position;
        state = State.Roam;
        roamPos = GetRoamingPos();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            health -= 25;
            if (health <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }
    private void Update()
    {
        if (roamPos.x > transform.position.x && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (roamPos.x < transform.position.x && isFacingRight)
        {
            FlipCharacter();
        }
        CheckPlayer();
        //CheckGround();
        switch (state)
        {
            case State.Moving:
                break;
            case State.Roam:
                if (Vector2.Distance(transform.position, roamPos) < reachedPositionDistance)
                {
                    roamPos = GetRoamingPos();
                    state = State.Roam;
                }
                else
                {
                    MoveToSpot();
                }
                break;
            case State.Follow:
                transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, speed * Time.deltaTime);
                break;
            default:
                break;
        }
    }
    private void CheckPlayer()
    {
        playerFound = Physics2D.Raycast(checkPlayer.position, transform.right, playerCheckDistance, playerLayer);
        if (playerFound)
        {
            state = State.Follow;
        }
        else
        {
            state = State.Roam;
        }
    }
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, groundLayer);
    }
    private void CheckEdge()
    {
        edgeFound = Physics2D.Raycast(checkEdge.position, -transform.up, edgeCheckDistance, groundLayer);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(checkGround.position, checkGroundRadius);
        Gizmos.DrawLine(checkPlayer.position, new Vector3(checkPlayer.position.x + playerCheckDistance, checkPlayer.position.y, checkPlayer.position.z));
        Gizmos.DrawLine(checkEdge.position, new Vector3(checkEdge.position.x, checkEdge.position.y - edgeCheckDistance, checkEdge.position.z));
    }
    public void ResetEnemy()
    {
        transform.position = startingPos;
    }
    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void MoveToSpot()
    {
        transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, roamSpeed * Time.deltaTime);
        state = State.Moving;
    }
    private Vector2 GetRoamingPos()
    {
        return new Vector2(startingPos.x + GetRandomDir().x, startingPos.y);
    }
    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-5.0f, 5.0f), transform.position.y);
    }
    private IEnumerator Die()
    {
        spr.enabled = false;
        bxc.enabled = false;
        ps.Play();
        yield return new WaitForSeconds(0.5f);
    }
}
