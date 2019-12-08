using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelEnding : MonoBehaviour
{
    public static UnityEvent levelEndingEvent = new UnityEvent();

    private void Start()
    {
        if (levelEndingEvent == null)
            levelEndingEvent = new UnityEvent();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            levelEndingEvent.Invoke();
        }
    }
}
