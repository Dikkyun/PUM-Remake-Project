using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public Camera mainCamera;

    private Vector3 originalCameraPosition;
    private float shakeIntensity = 0.2f;
    private float shakeDuration = 0.2f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera if not set in the Inspector
        }
        originalCameraPosition = mainCamera.transform.position;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;
        float originalZ = mainCamera.transform.position.z; // Keep the original Z position

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            float xOffset = Random.Range(-1f, 1f) * shakeIntensity;
            float yOffset = Random.Range(-1f, 1f) * shakeIntensity;

            // Keep Z constant, only shake X and Y
            mainCamera.transform.position = new Vector3(originalCameraPosition.x + xOffset, originalCameraPosition.y + yOffset, originalZ);
            yield return null;
        }

        // Reset only X and Y positions, keep Z unchanged
        mainCamera.transform.position = new Vector3(originalCameraPosition.x, originalCameraPosition.y, originalZ);
    }
}

   