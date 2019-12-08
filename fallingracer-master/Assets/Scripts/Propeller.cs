using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField] float speed = 500f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * speed * Time.deltaTime, Space.Self);     
    }
}
