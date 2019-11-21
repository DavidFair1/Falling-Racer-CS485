using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloudMovement());
    }

    private IEnumerator CloudMovement()
    {
        float timePassed = 0;
        int direction = 1;

        while (true)
        {
            while (timePassed < 3)
            {
                transform.Translate(Vector3.right * 3 * direction * Time.deltaTime, Space.World);
                timePassed += Time.deltaTime;
                yield return null;
            }
            direction = direction * -1;
            timePassed = 0;
        }
    }
}
