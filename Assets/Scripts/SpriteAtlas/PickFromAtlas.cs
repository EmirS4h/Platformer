using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class PickFromAtlas : MonoBehaviour
{
    [SerializeField] private string spriteName;
    [SerializeField] private SpriteAtlas spriteAtlas;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteAtlas.GetSprite(spriteName);
    }
}
