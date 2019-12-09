using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    Vector3 fullScale = new Vector3(0f, 0f, .0003f);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(.0003f, .0003f, 0f);
    }

    private void OnEnable()
    {
        StartCoroutine(GrowToSize());
    }

    private IEnumerator GrowToSize()
    {
        float timePassed = 0f;
        while(timePassed < 1f)
        {
            gameObject.transform.localScale += fullScale * Time.deltaTime;
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
}
