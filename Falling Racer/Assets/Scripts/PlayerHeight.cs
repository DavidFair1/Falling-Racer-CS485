using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for keeping track of the player's distance away from the end goal
/// </summary>
public class PlayerHeight : MonoBehaviour
{
    [SerializeField] private Text heightText;
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Transform playerHeight;
    private float startHeight = 1000;
    private bool playerIsAlive = true;


    private void Start()
    {
        PlayerMovement.playerDestroyedEvent.AddListener(PlayerIsGone);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAlive)
            UpdateHeight();      
    }

    private void UpdateHeight()
    {
        float currentHeight = playerHeight.position.y + 900;
        heightText.text = "Height: " + System.Math.Round(currentHeight, 2) + "m";

        heightSlider.value = 1 - currentHeight / startHeight;
    }

    private void PlayerIsGone()
    {
        playerIsAlive = false;
    }
}
