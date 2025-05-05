using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] Transform ScannerPivot;

    public delegate void OnScanDetectionUpdated(GameObject newDetection);
    public event OnScanDetectionUpdated onScanDetectionUpdated;

    [SerializeField] float scanRange;
    [SerializeField]private float scanDuration;

    internal void SetScanRange(float scanRange)
    {
        this.scanRange = scanRange;
    }
    internal void SetScanDuration(float duration)
    {
        scanDuration = duration;
    }

    internal void AddChildAttached(Transform newChild)
    {
        newChild.parent = ScannerPivot;
        newChild.localPosition = Vector3.zero;
    }

    internal void StartScan()
    {
        ScannerPivot.localScale = Vector3.zero;
        StartCoroutine(StartScanCoroutine());
    }

    IEnumerator StartScanCoroutine()
    {
        float scanGrowthRate = scanRange / scanDuration;
        float startTime = 0;
        while (startTime < scanDuration)
        {
            startTime += Time.deltaTime;
            ScannerPivot.localScale += Vector3.one * scanGrowthRate * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        onScanDetectionUpdated?.Invoke(other.gameObject);
    }
}
