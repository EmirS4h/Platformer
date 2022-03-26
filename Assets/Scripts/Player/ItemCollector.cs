using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI appleCountText;
    private int applesGathered = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            applesGathered++;
            appleCountText.text = "Apples " + applesGathered;
        }
    }
}
