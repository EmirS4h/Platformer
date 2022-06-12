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
        }
    }
}
