using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.right;
    [SerializeField] float speed = 3f;
    [SerializeField] float moveInterval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloudMovement());
    }

    private IEnumerator CloudMovement()
    {
        float timePassed = 0;
        int directionInverter = 1;

        while (true)
        {
            while (timePassed < moveInterval)
            {
                transform.Translate(direction * speed * directionInverter * Time.deltaTime, Space.World);
                timePassed += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(.3f);
            directionInverter = directionInverter * -1;
            timePassed = 0;
        }
    }
}
