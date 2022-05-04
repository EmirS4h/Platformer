using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "Boost", order = 1)]
public class Boost : ScriptableObject
{
    public enum Type
    {
        JumpForce,
        DashForce,
        MoveSpeed,
        DoubleJumpBooster,
        DoubleDashBooster,
        PermaDashForce,
        PermaMoveSpeed,
        PermaJumpForce,
    }
    public Type type;
    public float boostAmount;
    public float boostTime;

    public float permaMoveSpeedBoostAmount;
    public float permaDashForceBoostAmount;
    public float permaJumpForceBoostAmount;
}
