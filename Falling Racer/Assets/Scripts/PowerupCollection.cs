using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for cleaning up powerups as they are collected
/// </summary>
public class PowerupCollection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
