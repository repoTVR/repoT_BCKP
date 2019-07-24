using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSViewer : MonoBehaviour
{

    private int FramesPerSec;
    private float frequency = 1.0f;
    private string fps;
    private TextMeshProUGUI text;



    void Start()
    {
        StartCoroutine(FPS());
        text = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it

            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
            text.SetText(fps);
        }
    }
}
