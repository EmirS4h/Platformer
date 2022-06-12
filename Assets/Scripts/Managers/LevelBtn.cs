using TMPro;
using UnityEngine;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private TextMeshProUGUI selectedLevelText;
    public void SetLevel()
    {
        selectedLevelText.SetText("Level " + levelIndex.ToString());
        PlayerPrefs.SetInt("selectedLevelIndex", levelIndex);
    }
}
