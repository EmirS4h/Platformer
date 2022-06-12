using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    [SerializeField] private LayerMask roomLayer;
    [SerializeField] private LevelGeneration levelGeneration;
    private int rnd = 0;
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, roomLayer);

        if (roomDetection == null && levelGeneration.stopGeneration)
        {
            rnd = Random.Range(0, levelGeneration.rooms.Length);
            Instantiate(levelGeneration.rooms[rnd], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
