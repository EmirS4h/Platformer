using TMPro;
using UnityEngine;
public class ItemCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] CardDescription cardDescription;
    [SerializeField] private PlayerActions playerActions;

    private void Awake()
    {
        SetTexts();
    }

    private void OnEnable()
    {
        playerActions.optionsBtn += CloseItemCard;
    }
    private void OnDisable()
    {
        playerActions.optionsBtn -= CloseItemCard;
    }

    private void CloseItemCard()
    {
        gameObject.SetActive(false);
        UiManager.Instance.activeUi = UiManager.UI.Hud;
        GameManager.Instance.StartTime();
    }

    public void SetTexts()
    {
        header.SetText(cardDescription.HeaderText);
        description.SetText(cardDescription.DescriptionText);
    }

}
