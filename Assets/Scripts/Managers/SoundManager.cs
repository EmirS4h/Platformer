using UnityEngine;

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
}
