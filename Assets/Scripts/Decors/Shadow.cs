using UnityEngine;
using UnityEngine.U2D;

public class Shadow : MonoBehaviour
{
    [SerializeField] float fadeTime = 1.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FadeOut();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FadeIn();
    }
    private void FadeIn()
    {
        LeanTween.alpha(gameObject, 1.0f, fadeTime);
    }
    private void FadeOut()
    {
        LeanTween.alpha(gameObject, 0.0f, fadeTime);
    }
}
