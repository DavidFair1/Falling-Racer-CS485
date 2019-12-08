using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScreen : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    public void NextLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex + 1);
    }
}
