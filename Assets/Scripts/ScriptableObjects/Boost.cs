using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "Boost", order = 1)]
public class Boost : ScriptableObject
{
    public enum Type
    {
        DoubleJumpBooster,
        DoubleDashBooster,
        PermaDashForce,
        PermaMoveSpeed,
        PermaJumpForce,
    }
    public Type type;

    public float permaMoveSpeedBoostAmount;
    public float permaDashForceBoostAmount;
    public float permaJumpForceBoostAmount;
}
