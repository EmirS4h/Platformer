using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private Transform[] startingPositions;
    [SerializeField] private LayerMask roomLayer;
    [SerializeField] private GameObject player;
    // Room Types Based On Index
    // Index[0] = LR
    // Index[1] = LRB
    // Index[2] = LRT
    // Index[3] = LRTB
    public GameObject[] rooms;

    // Direction the room will go
    [SerializeField] private int direction;
    [SerializeField] private int moveAmount; // size of room


    [SerializeField] private float timeBtwRooms; // size of room
    [SerializeField] private float startTimeBtwRooms = 0.25f; // size of room

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;

    public bool stopGeneration = false;

    [SerializeField] private int downCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        int randomStartingPosition = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randomStartingPosition].position;
        player.transform.position = startingPositions[randomStartingPosition].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRooms < 0 && !stopGeneration)
        {
            Move();
            timeBtwRooms = startTimeBtwRooms;
        }
        else
        {
            timeBtwRooms -= Time.deltaTime;
        }
    }

    private void Move()
    {
        // Moving Right
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rnd = Random.Range(0, rooms.Length);
                Instantiate(rooms[rnd], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);

                if (direction == 3)
                    direction = 2;
                else if (direction == 4)
                    direction = 5;
            }
            // reached end of the col
            else
            {
                direction = 5;
            }
        }
        // Moving Left
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rnd = Random.Range(0, rooms.Length);
                Instantiate(rooms[rnd], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            // reached end of the col
            else
            {
                direction = 5;
            }
        }
        // Moving Down
        else if (direction == 5)
        {
            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);

                if (roomDetection.GetComponent<RoomType>().roomType != 1 && roomDetection.GetComponent<RoomType>().roomType != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().DestroyRoom();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().DestroyRoom();

                        int rndBottomRoom = Random.Range(1, 4);
                        if (rndBottomRoom == 2)
                        {
                            rndBottomRoom = 3;
                        }
                        Instantiate(rooms[rndBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rnd = Random.Range(2, 4);
                Instantiate(rooms[rnd], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                roomDetection.enabled = false;
            }
            else
            {
                // Stop the Level Gen
                stopGeneration = true;
                StartCoroutine(SpawnPlayer());
            }
        }
    }
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
    }
}
