using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEnemy : MonoBehaviour
{
    Vector3 startPosition;

    private void Start()
    {
        startPosition = new Vector3(-15f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up * 10 * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundaries")
            transform.position = startPosition;
        else if(other.gameObject.tag == "Player")
        {
            RestartLevel(other.gameObject);
        }
    }

    private void RestartLevel(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
