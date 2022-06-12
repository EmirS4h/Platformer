using UnityEngine;
using UnityEngine.UI;
public class ButtonSelect : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Button[] buttons;
    [SerializeField] bool mainMenu;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (mainMenu && buttons.Length > 0)
        {
            buttons[PlayerPrefs.HasKey("selectedLevelIndex") ? PlayerPrefs.GetInt("selectedLevelIndex") - 1 : 0].Select();
        }
        else
        {
            button.Select();
        }
    }
}
