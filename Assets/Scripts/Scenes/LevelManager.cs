using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.playerHaveTheKey)
        {
            StartCoroutine(LoadNextScene());
        }
    }
    IEnumerator LoadNextScene()
    {
        player.isGameOver = true;
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
