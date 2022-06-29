using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] private TextMeshProUGUI timerText;

    private float timerTime;
    private float t;

    private string minutes;
    private string seconds;

    private string timeText;

    [SerializeField] private GameObject mainMenu, pauseMenu, optionsMenu, charSelectMenu, Hud, itemCard, notifier, chars, upgradeCard, levelSelectScreen;

    public GameObject loadingScreen;
    public Slider slider;

    [SerializeField] private GameObject background;

    [SerializeField] private GameObject levelComplete;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI levelTimeText;
    [SerializeField] private TextMeshProUGUI selectedLevelText;


    [SerializeField] private TextMeshProUGUI notifTitle;
    [SerializeField] private TextMeshProUGUI notifDescription;

    [SerializeField] private CharSelection charSelection;

    [SerializeField] private PlayerActions playerActions;

    [SerializeField] private int selectedCharIndex = 0;

    [SerializeField] private Image potImage;

    [SerializeField] AudioSource musicSource;
    [SerializeField] private Slider musicSlider;

    public enum UI
    {
        MainMenu,
        PauseMenu,
        OptionsMenu,
        CharacterSelectMenu,
        Hud,
        itemCard,
        upgradeCard,
        loadingScreen,
        levelSelectScreen,
    }
    public UI activeUi;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            selectedCharIndex = PlayerPrefs.GetInt("charIndex");
            if (chars != null)
                chars.transform.GetChild(selectedCharIndex).gameObject.SetActive(true);
            SoundManager.Instance.LoadMusicVolume(musicSource, musicSlider);
        }
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            playerActions.optionsBtn += OpenPauseMenu;
            playerActions.optionsBtn += SoundPitchDown;
        }
    }
    private void OnDisable()
    {
        playerActions.optionsBtn -= OpenPauseMenu;
        playerActions.optionsBtn -= SoundPitchDown;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            selectedLevelText.SetText(PlayerPrefs.HasKey("selectedLevelIndex") ? "Level " + PlayerPrefs.GetInt("selectedLevelIndex").ToString() : "Level 1");
            playerActions.DisableMovement();
            OpenMainMenu();
        }
        else
        {
            chars = null;
            timerTime = Time.time;
        }
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

    public void ActivateLoadingScreen()
    {
        activeUi = UI.loadingScreen;
        playerActions.DisableMovement();
        loadingScreen.SetActive(true);
    }

    public void DeactivateLoadingScreen()
    {
        activeUi = UI.Hud;
        playerActions.EnableMovement();
        loadingScreen.SetActive(false);
    }

    public void StartBtn()
    {
        charSelection.SetChar();
        playerActions.EnableMovement();
        GameManager.Instance.StartTime();
        LevelManager.Instance.LoadLevel(PlayerPrefs.HasKey("selectedLevelIndex") ? PlayerPrefs.GetInt("selectedLevelIndex") : 1);
    }
    public void SavePlayerData()
    {
        PlayerController.Instance.SavePlayerData();
    }
    public void ReturnToMainMenu()
    {
        LevelManager.Instance.LoadMainMenu();
    }
    public void ContinueBtn()
    {
        activeUi = UI.Hud;
        playerActions.EnableMovement();
        background.SetActive(false);
        GameManager.Instance.StartTime();
        SoundPitchDefault();
    }
    public void ResetBtn()
    {
        PlayerController.Instance.ResetPlayerData();
        PlayerController.Instance.SavePlayerData();
    }
    public void ActivateItemCard()
    {
        playerActions.DisableMovement();
        itemCard.SetActive(true);
        activeUi = UI.itemCard;
        GameManager.Instance.StopTime();
    }
    public void DeactivateItemCard()
    {
        itemCard.SetActive(false);
        activeUi = UI.Hud;
        GameManager.Instance.StartTime();
    }

    public void ActivateUpgradeCard()
    {
        playerActions.DisableMovement();
        upgradeCard.SetActive(true);
        activeUi = UI.upgradeCard;
        GameManager.Instance.StopTime();
    }
    public void DeactivateUpgradeCard()
    {
        upgradeCard.SetActive(false);
        activeUi = UI.Hud;
        GameManager.Instance.StartTime();
    }

    public void Notif(string title, string details)
    {
        playerActions.DisableMovement();
        notifTitle.SetText(title);
        notifDescription.SetText(details);
        notifier.SetActive(true);
        activeUi = UI.itemCard;
        GameManager.Instance.StopTime();
    }
    public void CloseNotif()
    {
        playerActions.EnableMovement();
        notifier.SetActive(false);
        activeUi = UI.Hud;
        GameManager.Instance.StartTime();
    }
    public void QuitGame()
    {
        PlayerController.Instance.SavePlayerData();
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        playerActions.DisableMovement();
        activeUi = UI.MainMenu;
        mainMenu.SetActive(true);
    }
    public void CloseMainMenu()
    {
        mainMenu.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        playerActions.DisableMovement();
        activeUi = UI.OptionsMenu;
        optionsMenu.SetActive(true);
    }
    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        background.SetActive(true);
        GameManager.Instance.StopTime();
        playerActions.DisableMovement();
        activeUi = UI.PauseMenu;
        playerActions.DisableMovement();
        pauseMenu.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        playerActions.EnableMovement();
        pauseMenu.SetActive(false);
    }

    public void OpenCharSelectMenu()
    {
        playerActions.DisableMovement();
        activeUi = UI.CharacterSelectMenu;
        chars.transform.position = new Vector2(0.0f, 0.0f);
        charSelectMenu.SetActive(true);
    }
    public void CloseCharSelectMenu()
    {
        chars.transform.position = new Vector2(-5.81f, 0.0f);
        charSelectMenu.SetActive(false);
    }

    public void OpenLevelSelectScreen()
    {
        playerActions.DisableMovement();
        activeUi = UI.levelSelectScreen;
        CloseMainMenu();
        levelSelectScreen.SetActive(true);
    }

    public void CloseLevelSelectScreen()
    {
        activeUi = UI.MainMenu;
        OpenMainMenu();
        levelSelectScreen.SetActive(false);
    }

    public void CloseChar()
    {
        chars.SetActive(false);
    }
    public void OpenChar()
    {
        chars.SetActive(true);
    }

    public void SoundPitchDown()
    {
        GameManager.Instance.PitchSoundDown();
    }
    public void SoundPitchDefault()
    {
        GameManager.Instance.DefaultPitch();
    }
    public void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
    public void LevelEnd()
    {
        GameManager.Instance.StopTime();
        playerActions.DisableMovement();
        levelTimeText.SetText(timeText);
        currentLevelText.SetText("LEVEL "+SceneManager.GetActiveScene().buildIndex.ToString());
        levelComplete.SetActive(true);
    }
    public void ActivatePotImage(Sprite sprite)
    {
        potImage.enabled = true;
        potImage.sprite = sprite;
    }
    public void DeactivatePotImage()
    {
        potImage.enabled = false;
    }
    public void ChangeMusicVolume()
    {
        SoundManager.Instance.ChangeMusicVolume(musicSource, musicSlider);
    }
}
