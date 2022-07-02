using UnityEngine;

public class BoostCounter : MonoBehaviour
{
    public enum CounterType
    {
        DoubleJump,
        DoubleDash,
        PermaMoveSpeedForce,
        PermaDashForce,
        PermaJumpForce
    }
    public CounterType type;

    [SerializeField] Sprite zero;
    [SerializeField] Sprite first;
    [SerializeField] Sprite second;
    [SerializeField] Sprite third;

    SpriteRenderer spr;

    private void Awake()
    {
    }
}
