using System.Linq;
using UnityEngine;
public class LevelKey : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Sprite inActiveSprite;
    [SerializeField] Sprite activeSprite;
    [SerializeField] GameObject bg, interactBtn;
    SpriteRenderer spriteRenderer;

    [SerializeField] bool playerOnKey = false;
    public bool keyActivated = false;

    [SerializeField] PlayerActions playerActions = default;

    [SerializeField] GameObject portalOrb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactBtn = transform.GetChild(2).gameObject;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        playerActions.interactEvent += ActivateKey;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= ActivateKey;
    }
    public void ActivateKey()
    {
        if (playerOnKey)
        {
            spriteRenderer.sprite = activeSprite;
            particles.Play();
            audioSource.Play();
            bg.SetActive(true);
            keyActivated = true;
            GameManager.Instance.keysActivated.Add(this); // Adds its self to GameManagers activeKeysList
            GameManager.Instance.totemsActivated = GameManager.Instance.keys.All(key => key.keyActivated == true);
            if (GameManager.Instance.totemsActivated)
            {
                GameManager.Instance.SpawnPortal();
                Instantiate(portalOrb, transform.position, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnKey = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerOnKey = true;
        if (!keyActivated)
        {
            interactBtn.SetActive(true);
        }
        else
        {
            interactBtn.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnKey = false;
        interactBtn.SetActive(false);
    }

    public void DeactivateKey()
    {
        spriteRenderer.sprite = inActiveSprite;
        bg.SetActive(false);
        keyActivated = false;
        particles.Stop();
    }
}
