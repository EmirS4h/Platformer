using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    [SerializeField] Light2D _light;
    [SerializeField] bool flickIntensity;
    [SerializeField] float _baseIntensity;
    [SerializeField] float intensityRange;
    [SerializeField] float intensityTimeMin;
    [SerializeField] float intensityTimeMax;
    [SerializeField] float secondsBetweenFlickers;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {
        StartCoroutine(TimerLight());
    }

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));

        while (true)
        {
            if (flickIntensity)
            {
                t0 = Time.time;
                float r = Random.Range(_baseIntensity - intensityRange, _baseIntensity + intensityRange);
                _light.intensity = r;
                t = Random.Range(intensityTimeMin, intensityTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
    IEnumerator TimerLight()
    {
        while (true)
        {
            _light.intensity = Random.Range(intensityTimeMin, intensityTimeMax);
            var randomTime = Random.Range(0.8f, secondsBetweenFlickers);
            yield return new WaitForSeconds(randomTime);

        }
    }
}
