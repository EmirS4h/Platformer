using UnityEngine;

public class CharSelection : MonoBehaviour
{
    [SerializeField] GameObject[] chars;
    public int selectedCharIndex = 0;

    private void Awake()
    {
        selectedCharIndex = PlayerPrefs.GetInt("charIndex");
    }

    private void Start()
    {
        chars[selectedCharIndex].SetActive(true);
    }
    public void NextCharacter()
    {
        chars[selectedCharIndex].SetActive(false);
        selectedCharIndex = (selectedCharIndex + 1) % chars.Length;
        chars[selectedCharIndex].SetActive(true);
    }
    public void PreviousCharacter()
    {
        chars[selectedCharIndex].SetActive(false);
        selectedCharIndex--;
        if (selectedCharIndex < 0)
        {
            selectedCharIndex += chars.Length;
        }
        chars[selectedCharIndex].SetActive(true);
    }

    // Set the Prefferd Char Selection
    public void SetChar()
    {
        PlayerPrefs.SetInt("charIndex", selectedCharIndex);
    }
}
