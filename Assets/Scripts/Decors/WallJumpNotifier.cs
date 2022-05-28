using System.Collections;
using System.Collections.Generic;
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
        ChestNotif,
    }
    public NotifType type;

    [SerializeField] NotifDetails notifDetails;

    private void Awake()
    {
        PlayerPrefs.DeleteKey("wallJumpNotif");
        PlayerPrefs.DeleteKey("dashNotif");
    }
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
        }
    }
}
