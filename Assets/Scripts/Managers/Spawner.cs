using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Spawner : MonoBehaviour
{
    [SerializeField] private ApplyBoost potion;
    [SerializeField] private int spawnAmount;

    private ObjectPool<ApplyBoost> _pool;
    private void Awake()
    {
        _pool = new ObjectPool<ApplyBoost>(() =>
        {
            return Instantiate(potion); // To Create an Object
        },
        pot =>
        {
            pot.gameObject.SetActive(true); // To Get an Object
            pot.gameObject.transform.position = transform.position + Random.onUnitSphere * 5.0f;
        },
        pot =>
        {
            pot.gameObject.SetActive(false); // To Deactivate an Object
        },
        pot =>
        {
            Destroy(pot.gameObject); // To Destroy an Object
        },
        false,
        10, // Default size
        20); // Max size
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            _pool.Get();
        }
    }
}
