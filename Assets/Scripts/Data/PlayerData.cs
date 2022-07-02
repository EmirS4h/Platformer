[System.Serializable]
public class PlayerData
{
    public float playerSpeed;
    public float playerJumpForce;
    public float playerDashForce;

    public bool hasDoubleJump;
    public int playerMaxDash;

    public bool collectedDashBoosterAmount;
    public bool collectedJumpBoosterAmount;

    public bool collectedDashForceBoosterAmount;
    public bool collectedJumpForceBoosterAmount;
    public bool collectedMoveSpeedBoosterAmount;

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

        collectedDashBoosterAmount = player.collectedDoubleDashBooster;
        collectedJumpBoosterAmount = player.collectedDoubleJumpBooster;

        collectedDashForceBoosterAmount = player.collectedDashForceBooster;
        collectedJumpForceBoosterAmount = player.collectedJumpForceBooster;
        collectedMoveSpeedBoosterAmount = player.collectedMoveSpeedBooster;

        hasCollectedPowerUpStone = player.hasCollectedPowerUpStone;
    }
}
