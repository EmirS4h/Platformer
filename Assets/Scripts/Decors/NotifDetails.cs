using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotifDetail", menuName = "Notifier", order = 1)]
public class NotifDetails : ScriptableObject
{
    public string title;
    [TextArea]
    public string description;
}
