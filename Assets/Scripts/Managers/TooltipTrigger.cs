using UnityEngine;
using UnityEngine.EventSystems;
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static LTDescr delay;
    [SerializeField] string content;
    [SerializeField] string header;
    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
         {
             TooltipSystem.Instance.Show(content, header);
         });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Instance.Hide();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        delay = LeanTween.delayedCall(0.2f, () =>
        {
            TooltipSystem.Instance.Show(content, header);
        });
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Instance.Hide();
    }
}
