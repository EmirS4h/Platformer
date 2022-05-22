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
            return Instantiate(potion);
        },
        pot =>
        {
            pot.gameObject.SetActive(true);
            pot.gameObject.transform.position = transform.position + Random.onUnitSphere * 5.0f;
        },
        pot =>
        {
            pot.gameObject.SetActive(false);
        },
        pot =>
        {
            Destroy(pot.gameObject);
        },
        false,
        50,
        50);
    }

    private void Start()
    {
        Invoke(nameof(Spawn), 5.0f);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            _pool.Get();
        }
    }
}
