using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] float shakeMag = 0.1f;
    [SerializeField] float shakeDuration = 0.1f;
    [SerializeField] Transform shakeTransform;
    [SerializeField] float shakeRecoverySpeed = 10f;


    Coroutine ShakeCoroutine;
    bool isShaking;

    Vector3 originalPos;
   // Quaternion originalRot;

    void Start()
    {
        originalPos = transform.localPosition;
    //    originalRot = transform.rotation;
    }

    public void StartShake()
    {
        if (ShakeCoroutine == null)
        {
            isShaking = true;
            ShakeCoroutine = StartCoroutine(ShakeStarted());
        }
    }

    IEnumerator ShakeStarted()
    {
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
        ShakeCoroutine = null;
    }
    private void LateUpdate()
    {
        ProcessShake();
    }

    void ProcessShake()
    {
        if (isShaking)
        {
            Vector3 ShakeAmt = new Vector3(Random.value, Random.value, Random.value) * shakeMag * (Random.value > 0.5 ? -1 : 1);
            shakeTransform.localPosition += ShakeAmt;
        }
        else
        {
            shakeTransform.localPosition = Vector3.Lerp(shakeTransform.localPosition, originalPos, Time.deltaTime * shakeRecoverySpeed);
        }
    }


}
