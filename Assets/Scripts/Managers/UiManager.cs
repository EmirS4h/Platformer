using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Canvas btnUI;
    [SerializeField] Canvas menuUI;
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

        t = Time.time - timerTime;

        minutes = ((int)t / 60).ToString();
        seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":"+seconds;
    }
    public void PauseTheGameBtn()
    {

        menuUI.enabled = !menuUI.enabled;
        if (menuUI.enabled)
        {
            Time.timeScale = 0.0f;
            btnUI.enabled = false;
        }
        else
        {
            Time.timeScale = 1.0f;
            btnUI.enabled = true;
        }
    }

    public void PauseTheGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuUI.enabled = !menuUI.enabled;
            if (menuUI.enabled)
            {
                Time.timeScale = 0.0f;
                btnUI.enabled = false;
            }
            else
            {
                Time.timeScale = 1.0f;
                btnUI.enabled = true;
            }
        }
    }
}
