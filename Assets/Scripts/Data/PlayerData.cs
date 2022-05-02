using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float playerSpeed;
    public float playerJumpForce;
    public float playerDashForce;

    public bool hasDoubleJump;
    public int playerMaxDash;

    public int collectedDashBoosterAmount;
    public int collectedJumpBoosterAmount;
    public int collectedDashForceBoosterAmount;

    public PlayerData(PlayerController player)
    {
        playerSpeed = player.moveSpeed;
        playerJumpForce = player.jumpForce;
        playerDashForce = player.dashForce;

        hasDoubleJump = player.hasDoubleJump;

        playerMaxDash = player.maxDash;

        collectedDashBoosterAmount = player.collectedDashBoosterAmount;
        collectedJumpBoosterAmount = player.collectedJumpBoosterAmount;
        collectedDashForceBoosterAmount = player.collectedDashForceBoosterAmount;

    }
}
