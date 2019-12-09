using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField] Transform customPivot;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(customPivot.position, Vector3.right, 1000 * Time.deltaTime);
    }
}
