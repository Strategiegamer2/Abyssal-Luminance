using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightIntensityControl : MonoBehaviour
{
    public Light2D targetLight; // Assign your light component here
    public float transitionDuration = 2f; // Duration of the intensity change
    private bool isIncreasingIntensity = false;
    private float elapsedTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isIncreasingIntensity = true;
            elapsedTime = 0f; // Reset the time elapsed for the transition
        }

        if (isIncreasingIntensity)
        {
            if (elapsedTime < transitionDuration)
            {
                targetLight.intensity = Mathf.Lerp(0f, 1f, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                targetLight.intensity = 1f; // Ensure final intensity is set to 1
                isIncreasingIntensity = false; // Stop the transition
            }
        }
    }
}
