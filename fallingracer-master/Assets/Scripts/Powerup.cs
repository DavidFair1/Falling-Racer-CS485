using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for cleaning up powerups as they are collected
/// </summary>
public class Powerup : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y + 2f, transform.rotation.z, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
