using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Level yukleme animasyonunu oynatýr ve bir sonraki leveli yukler
    public IEnumerator LoadNextLevel()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1);
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    // Level yukleme animasyonunu oynatýr ve istenilen leveli yukler
    public IEnumerator LoadLevel(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1);
        if (SceneManager.sceneCountInBuildSettings >= index)
        {
            SceneManager.LoadScene(index);
        }
    }

    // Ilk leveli yukler
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadLevel(1));
    }
    // Cikis yapar
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
}
