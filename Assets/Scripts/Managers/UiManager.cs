using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] TextMeshProUGUI timerText;

    float timerTime;
    float t;

    string minutes;
    string seconds;

    string timeText;

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
            seconds = (t % 60).ToString("f0");
            timeText = minutes + ":"+seconds;
            timerText.text = timeText;
        }
    }
    
    public void LevelEnd()
    {
        levelEndingTimeText.text = timeText;
    }
    public void NextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
    public void Save()
    {
        PlayerController.Instance.SavePlayerData();
    }
}
