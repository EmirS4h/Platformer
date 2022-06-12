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
    public int collectedJumpForceBoosterAmount;
    public int collectedMoveSpeedBoosterAmount;

    public bool hasCollectedDoubleDashBooster = false;
    public bool hasCollectedDoubleJumpBooster = false;

    public bool hasCollectedDashForceBooster = false;
    public bool hasCollectedJumpForceBooster = false;
    public bool hasCollectedMoveSpeedBooster = false;

    public bool hasCollectedPowerUpStone = false;

    public PlayerData(PlayerController player)
    {
        playerSpeed = player.moveSpeed;
        playerJumpForce = player.jumpForce;
        playerDashForce = player.dashForce;

        hasDoubleJump = player.hasDoubleJump;

        playerMaxDash = player.maxDash;

        collectedDashBoosterAmount = player.collectedDoubleDashBoosterAmount;
        collectedJumpBoosterAmount = player.collectedDoubleJumpBoosterAmount;

        collectedDashForceBoosterAmount = player.collectedDashForceBoosterAmount;
        collectedJumpForceBoosterAmount = player.collectedJumpForceBoosterAmount;
        collectedMoveSpeedBoosterAmount = player.collectedMoveSpeedBoosterAmount;

        hasCollectedPowerUpStone = player.hasCollectedPowerUpStone;
    }
}
