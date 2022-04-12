using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            if (canvas.enabled)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;
        }
    }
}
