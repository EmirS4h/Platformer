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

    public void SetTexts()
    {
        header.SetText(cardDescription.HeaderText);
        description.SetText(cardDescription.DescriptionText);
    }

}
