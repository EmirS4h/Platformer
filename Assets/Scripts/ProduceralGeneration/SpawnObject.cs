using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    void Start()
    {
        int rand = Random.Range(0, gameObjects.Length);
        
        GameObject instance = Instantiate(gameObjects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
