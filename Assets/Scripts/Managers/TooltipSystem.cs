using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;
    [SerializeField] Tooltip tooltip;
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
    public void Show(string content, string header = "")
    {

        Instance.tooltip.SetText(content, header);
        Instance.tooltip.gameObject.SetActive(true);
    }
    public void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
