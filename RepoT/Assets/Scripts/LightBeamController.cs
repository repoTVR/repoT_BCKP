using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : MonoBehaviour
{
    Light light;
    float duration;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        duration = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration);
        light.intensity = Mathf.Lerp(1, 5, t);
    }
}
