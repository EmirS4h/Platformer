using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] CardDescription cardDescription;

    private void Awake()
    {
        SetTexts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
            UiManager.Instance.activeUi = UiManager.UI.Hud;
            GameManager.Instance.StartTime();
        }
    }

    public void SetTexts()
    {
        header.SetText(cardDescription.HeaderText);
        description.SetText(cardDescription.DescriptionText);
    }

}
