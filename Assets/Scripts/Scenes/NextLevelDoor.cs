using UnityEngine;
public class NextLevelDoor : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] PlayerActions playerActions;
    [SerializeField] GameObject interactBtn;

    public bool isOnDoor = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        interactBtn = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        playerActions.interactEvent += EnterDoor;
    }

    private void OnDisable()
    {
        playerActions.interactEvent -= EnterDoor;
    }

    private void EnterDoor()
    {
        if (isOnDoor)
        {
            if (GameManager.Instance.totemsActivated)
            {
                interactBtn.SetActive(true);
                audioSource.Play();
                UiManager.Instance.LevelEnd();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnDoor = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isOnDoor = true;
        interactBtn.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnDoor = false;
        interactBtn.SetActive(false);
    }
}
