using System.Linq;
using UnityEngine;
public class LevelKey : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Sprite inActiveSprite;
    [SerializeField] Sprite activeSprite;
    [SerializeField] GameObject bg, interactBtn;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] bool playerOnKey = false;
    public bool keyActivated = false;

    [SerializeField] PlayerActions playerActions = default;

    [SerializeField] GameObject portalOrb;

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
        if (playerOnKey && !keyActivated)
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
                GameManager.Instance.portalOpened = true;
                GameManager.Instance.SpawnPortal();
                Instantiate(portalOrb, transform.position, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnKey = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!keyActivated)
            {
                interactBtn.SetActive(true);
            }
            else
            {
                interactBtn.SetActive(false);
            }
            playerOnKey = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnKey = false;
            interactBtn.SetActive(false);
        }
    }

    public void DeactivateKey()
    {
        spriteRenderer.sprite = inActiveSprite;
        bg.SetActive(false);
        keyActivated = false;
        particles.Stop();
    }
}
