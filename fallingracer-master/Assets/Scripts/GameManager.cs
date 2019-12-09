﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Fields")] 
    [SerializeField] private Canvas heightInfoCanvas;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private Text finalTimeText;
    [SerializeField] private Text bestTimeText;

    private float timeElapsed = 0;
    private float bestTime;

    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat("Best Time");
        LevelEnding.levelEndingEvent.AddListener(EndLevel);
        PlayerMovement.playerDestroyedEvent.AddListener(PlayerDeath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
        timeElapsed += Time.deltaTime;
    }

    private void EndLevel()
    {
        float finalTime = timeElapsed;

        if (bestTime == 0)
        {
            bestTime = finalTime;
            PlayerPrefs.SetFloat("Best Time", finalTime);
        }
        else
        {
            bestTime = Mathf.Min(bestTime, finalTime);
            PlayerPrefs.SetFloat("Best Time", bestTime);
        }
        finalTimeText.text = "Your Time: " + System.Math.Round(finalTime, 2) + "s";
        bestTimeText.text = "Best Time: " + System.Math.Round(bestTime, 2) + "s";
        heightInfoCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(true);
    }

    private void PlayerDeath()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3);
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }
}
