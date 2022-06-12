using UnityEngine;

[CreateAssetMenu(fileName = "Boost", menuName = "BoosterDescription", order = 2)]

public class CardDescription : ScriptableObject
{
    public string HeaderText;
    [TextArea]
    public string DescriptionText;
}
