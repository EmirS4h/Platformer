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

    [SerializeField] GameObject mainMenu, pauseMenu, optionsMenu, charSelectMenu, Hud;

    [SerializeField] Deneme deneme;

    public enum UI
    {
        MainMenu,
        PauseMenu,
        OptionsMenu,
        CharacterSelectMenu,
        Hud,
    }
    public UI activeUi;

    bool onMenu = false;
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
        if (Input.GetKeyDown(KeyCode.Escape) && activeUi != UI.MainMenu)
        {
            onMenu = !onMenu;
        }
        if (onMenu)
        {
            GameManager.Instance.StopTime();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null && activeUi != UI.OptionsMenu && activeUi != UI.MainMenu)
        {
            pauseMenu.SetActive(true);
            activeUi = UI.PauseMenu;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && activeUi == UI.OptionsMenu)
        {
            optionsMenu.SetActive(false);
            activeUi = UI.Hud;
            GameManager.Instance.StartTime();
        }
    }

    public void LevelEnd()
    {
        levelEndingTimeText.text = timeText;
    }
    public void NextLevel()
    {
        deneme.SetChar();
        StartCoroutine(LevelManager.Instance.LoadNextLevel());
    }
    public void Save()
    {
        PlayerController.Instance.SavePlayerData();
    }
    public void OptionsMenu()
    {
        activeUi = UI.OptionsMenu;
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void ReturnToMainMenu()
    {
        LevelManager.Instance.LoadMainMenu();
    }
    public void ContinueBtn()
    {
        onMenu = false;
        activeUi = UI.Hud;
        GameManager.Instance.StartTime();
    }
    public void ResetBtn()
    {
        PlayerController.Instance.ResetPlayerData();
        PlayerController.Instance.SavePlayerData();
    }
}
