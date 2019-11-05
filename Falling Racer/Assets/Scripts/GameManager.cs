using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text finalTimeText;
    [SerializeField] Text bestTimeText;
    [SerializeField] Canvas heightInfoCanvas;
    [SerializeField] Canvas endGameCanvas;
    [SerializeField] EndGame endGameEventHolder;
    private float timeElapsed = 0;
    private float bestTime;

    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat("Best Time");
        Debug.Log(bestTime);
        endGameEventHolder.endGame.AddListener(EndGame);
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;
    }

    private void EndGame()
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
        heightInfoCanvas.enabled = false;
        endGameCanvas.gameObject.SetActive(true);
    }
}
