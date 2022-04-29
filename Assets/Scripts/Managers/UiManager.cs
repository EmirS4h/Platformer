using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI timerText;

    float timerTime;
    float t;

    string minutes;
    string seconds;

    private void Start()
    {
        timerTime = Time.time;    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            if (canvas.enabled)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;
        }
        t = Time.time - timerTime;

        minutes = ((int)t / 60).ToString();
        seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":"+seconds;
    }
}
