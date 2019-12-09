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
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log(rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) > 20f)
                Destroy(collision.gameObject);
            else  
                levelEndingEvent.Invoke();
        }
    }
}
