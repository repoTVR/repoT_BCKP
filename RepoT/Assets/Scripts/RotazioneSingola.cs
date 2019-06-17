using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotazioneSingola : MonoBehaviour
{
    public bool rotDx;
    private float timeCount;
    private Quaternion rotation;
    private float oldRotation;
    private float targetRot;
    
    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0f;

        //Prendo la rotazione iniziale
        oldRotation = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRotazioneRaggiunta())
        {
            if (rotDx)
            {
                targetRot = oldRotation - 90f;
            }
            else
            {
                targetRot = oldRotation + 90f;
            }
            rotation = Quaternion.Euler(0, targetRot, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, timeCount * 0.1f);
            timeCount += Time.deltaTime;
        }
        else
        {
            timeCount = 0f;
            StartCoroutine("WaitBeforeNextAction");
        }
    }

    public bool IsRotazioneRaggiunta()
    {
        Debug.Log(Mathf.Abs((transform.localEulerAngles.y - targetRot)) <= 0.01f);
        return Mathf.Abs((transform.localEulerAngles.y - targetRot)) <= 0.01f;
    }

    private IEnumerator WaitBeforeNextAction()
    {
        ResetPosizione();
        yield return new WaitForSeconds(0.5f);
    }

    private void ResetPosizione()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
    }
}
