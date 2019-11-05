using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    public UnityEvent endGame = new UnityEvent();

    private void Start()
    {
        if (endGame == null)
            endGame = new UnityEvent();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EndGoal")
        {
            endGame.Invoke();
        }
    }
}
