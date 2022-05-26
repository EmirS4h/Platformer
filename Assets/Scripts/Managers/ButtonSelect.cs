using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSelect : MonoBehaviour
{
    [SerializeField] Button button;
    // Start is called before the first frame update
    void OnEnable()
    {
        button.Select();
    }
}
