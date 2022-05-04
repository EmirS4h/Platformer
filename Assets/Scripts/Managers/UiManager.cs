using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] Canvas btnUI;
    [SerializeField] Canvas menuUI;
    [SerializeField] TextMeshProUGUI timerText;

    float timerTime;
    float t;

    string minutes;
    string seconds;

    string timeText;

    [SerializeField] private Canvas levelEndCanvas;
    [SerializeField] private TextMeshProUGUI levelEndingTimeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timerTime = Time.time;
    }

    void Update()
    {
        if (!PlayerController.Instance.isGameOver)
        {
            t = Time.time - timerTime;

            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f2");
            timeText = minutes + ":"+seconds;
            timerText.text = timeText;
        }
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

    public void LevelEnd()
    {
        levelEndCanvas.enabled = true;
        levelEndingTimeText.text = timeText;
    }
}
