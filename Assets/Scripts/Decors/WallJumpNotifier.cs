using UnityEngine;

public class WallJumpNotifier : MonoBehaviour
{
    public enum NotifType
    {
        WallJumpNotif,
        PotionNotif,
        DashNotif,
        BoosterNotif,
        LevelKeyNotif,
        NextLevelDoorNotif,
        ChestNotif,
        PermaDashForceUpgradeNotif,
        PermaJumpForceUpgradeNotif,
        PermaMoveSpeedUpgradeNotif,
        DoubleDashUpgradeNotif,
        DoubleJumpUpgradeNotif,
        PlatformNotif,
        ComputerNotif,
        FlameNotif,
        LaserBeamNotif,
        TurretNotif,
        StonesNotif,
    }
    public NotifType type;

    [SerializeField] NotifDetails notifDetails;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (type)
        {
            case NotifType.WallJumpNotif:
                bool checkWallNotif = PlayerPrefs.HasKey("wallJumpNotif");
                if (!checkWallNotif)
                {
                    PlayerPrefs.SetInt("wallJumpNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.DashNotif:
                bool checkDashNotif = PlayerPrefs.HasKey("dashNotif");
                if (!checkDashNotif)
                {
                    PlayerPrefs.SetInt("dashNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.LevelKeyNotif:
                bool levelKeyNotif = PlayerPrefs.HasKey("totemNotif");
                if (!levelKeyNotif)
                {
                    PlayerPrefs.SetInt("totemNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.PotionNotif:
                bool potionNotif = PlayerPrefs.HasKey("potionNotif");
                if (!potionNotif)
                {
                    PlayerPrefs.SetInt("potionNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.NextLevelDoorNotif:
                bool nextLevelDoorNotif = PlayerPrefs.HasKey("nextLevelDoorNotif");
                if (!nextLevelDoorNotif)
                {
                    PlayerPrefs.SetInt("nextLevelDoorNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.PlatformNotif:
                if (!PlayerPrefs.HasKey("platformNotif"))
                {
                    PlayerPrefs.SetInt("platformNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.ComputerNotif:
                if (!PlayerPrefs.HasKey("computerNotif"))
                {
                    PlayerPrefs.SetInt("computerNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.FlameNotif:
                if (!PlayerPrefs.HasKey("flameNotif"))
                {
                    PlayerPrefs.SetInt("flameNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.LaserBeamNotif:
                if (!PlayerPrefs.HasKey("laserBeamNotif"))
                {
                    PlayerPrefs.SetInt("laserBeamNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.TurretNotif:
                if (!PlayerPrefs.HasKey("turretNotif"))
                {
                    PlayerPrefs.SetInt("turretNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
            case NotifType.StonesNotif:
                if (!PlayerPrefs.HasKey("stonesNotif"))
                {
                    PlayerPrefs.SetInt("stonesNotif", 1);
                    UiManager.Instance.Notif(notifDetails.title, notifDetails.description);
                }
                break;
        }
    }
}
