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
        spr = GetComponent<SpriteRenderer>();

        switch (type)
        {
            case CounterType.DoubleDash:
                switch (PlayerController.Instance.collectedDoubleDashBoosterAmount)
                {
                    case 0:
                        ChangeSprite(0);
                        break;
                    case 1:
                        ChangeSprite(1);
                        break;
                    case 2:
                        ChangeSprite(2);
                        break;
                    case 3:
                        ChangeSprite(3);
                        break;
                }
                break;
            case CounterType.DoubleJump:
                switch (PlayerController.Instance.collectedDoubleJumpBoosterAmount)
                {
                    case 0:
                        ChangeSprite(0);
                        break;
                    case 1:
                        ChangeSprite(1);
                        break;
                    case 2:
                        ChangeSprite(2);
                        break;
                    case 3:
                        ChangeSprite(3);
                        break;
                }
                break;
            case CounterType.PermaJumpForce:
                switch (PlayerController.Instance.collectedJumpForceBoosterAmount)
                {
                    case 0:
                        ChangeSprite(0);
                        break;
                    case 1:
                        ChangeSprite(1);
                        break;
                    case 2:
                        ChangeSprite(2);
                        break;
                    case 3:
                        ChangeSprite(3);
                        break;
                }
                break;
            case CounterType.PermaDashForce:
                switch (PlayerController.Instance.collectedDashForceBoosterAmount)
                {
                    case 0:
                        ChangeSprite(0);
                        break;
                    case 1:
                        ChangeSprite(1);
                        break;
                    case 2:
                        ChangeSprite(2);
                        break;
                    case 3:
                        ChangeSprite(3);
                        break;
                }
                break;
            case CounterType.PermaMoveSpeedForce:
                switch (PlayerController.Instance.collectedMoveSpeedBoosterAmount)
                {
                    case 0:
                        ChangeSprite(0);
                        break;
                    case 1:
                        ChangeSprite(1);
                        break;
                    case 2:
                        ChangeSprite(2);
                        break;
                    case 3:
                        ChangeSprite(3);
                        break;
                }
                break;
        }
    }

    private void ChangeSprite(int collectedAmount)
    {
        switch (collectedAmount)
        {
            case 0:
                spr.sprite = zero;
                break;
            case 1:
                spr.sprite = first;
                break;
            case 2:
                spr.sprite = second;
                break;
            case 3:
                spr.sprite = third;
                break;
            default:
                break;
        }
    }
}
