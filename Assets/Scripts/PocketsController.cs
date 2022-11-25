using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketsController : MonoBehaviour
{
    public GameObject cueBall;
    private Vector3 originalCueBallPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalCueBallPosition = cueBall.transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (cueBall.transform.name == collision.gameObject.name)
        {
            cueBall.transform.position = originalCueBallPosition;
        }
        else
        {
            var objectName = collision.gameObject.name;
            print(objectName);
            GameObject.Destroy(collision.gameObject);
        }
    }
}
