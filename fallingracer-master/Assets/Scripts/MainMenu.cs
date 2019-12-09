using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject characterSelectCanvas;
    [SerializeField] GameObject virtualCamera2;

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectCharacter()
    {
        characterSelectCanvas.SetActive(true);
        virtualCamera2.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
    		Application.Quit();
        #endif
    }
}
