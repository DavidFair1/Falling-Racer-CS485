using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {

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
}
