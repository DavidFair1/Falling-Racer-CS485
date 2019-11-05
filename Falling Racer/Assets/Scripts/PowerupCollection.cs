using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            Destroy(other.gameObject);
        }
    }
}
