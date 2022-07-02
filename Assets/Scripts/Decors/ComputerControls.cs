using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ComputerControls : MonoBehaviour
{
    [SerializeField] PlayerActions playerActions = default;

    [SerializeField] SpriteRenderer spr;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] Sprite activeSprite;

    [SerializeField] bool computerActivated = false;
    [SerializeField] bool playerOnComputer = false;
    [SerializeField] CastLaser[] lasers;

    [SerializeField] GameObject interactBtn;
    [SerializeField] Light2D pcLight;

    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        playerActions.interactEvent += ComputerInteraction;
    }
    private void OnDisable()
    {
        playerActions.interactEvent -= ComputerInteraction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnComputer = true;
            if (!computerActivated)
                interactBtn.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnComputer = false;
            interactBtn.SetActive(false);
        }
    }
    private void ComputerInteraction()
    {
        if (playerOnComputer && !computerActivated)
        {
            audioSource.Play();
            computerActivated = true;
            pcLight.enabled = true;
            spr.sprite = activeSprite;
            foreach (var item in lasers)
            {
                item.DeactivateLineRenderer();
            }
        }
        else if (playerOnComputer && computerActivated)
        {
            audioSource.Play();
            computerActivated = false;
            pcLight.enabled = false;
            spr.sprite = inactiveSprite;
            foreach (var item in lasers)
            {
                item.ActivateLineRenderer();
            }
        }
    }
    public void DeactivateComputer()
    {
        spr.sprite = inactiveSprite;
        computerActivated = false;
        pcLight.enabled = false;
    }
}
