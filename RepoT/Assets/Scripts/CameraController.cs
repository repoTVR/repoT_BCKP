using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    Vector3 pos;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
        offset = target.transform.position - pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position - offset;
    }
}
