using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;
    [Header("Jump Sound")]
    [SerializeField] AudioClip rockJumpSound;

    [Header("FootStep Sound")]
    [SerializeField] AudioClip footSteps;

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
        audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.2f);
        }
    }
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(rockJumpSound);
    }
    public void PlayFootStep()
    {
        audioSource.PlayOneShot(footSteps);
    }
    public void PlaySoundOneShot(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
    public void ChangeVolume(AudioSource source, Slider slider)
    {
        source.volume = slider.value;
        SaveMusicVolume(slider);
    }
    public void LoadMusicVolume(AudioSource source, Slider slider)
    {
        float vol = PlayerPrefs.GetFloat("musicVolume");
        source.volume = vol;
        slider.value =  vol;
    }
    public void SaveMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }
}
