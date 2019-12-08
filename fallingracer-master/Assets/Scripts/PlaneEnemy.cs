using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEnemy : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] float speed = 20f;
    [SerializeField] float moveInterval = 1f;
    [SerializeField] float floatingSpeed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaneFloating());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundaries")
            transform.position = new Vector3 (startPosition.position.x, transform.position.y, transform.position.z);
        else if(other.gameObject.tag == "Player")
        {
            RestartLevel(other.gameObject);
        }
    }

    private IEnumerator PlaneFloating()
    {
        float timePassed = 0;
        int directionInverter = 1;

        while (true)
        {
            while (timePassed < moveInterval)
            {
                transform.Translate(Vector3.up * floatingSpeed * directionInverter * Time.deltaTime, Space.World);
                timePassed += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(.2f);
            directionInverter = directionInverter * -1;
            timePassed = 0;
        }
    }

    private void RestartLevel(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
