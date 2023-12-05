using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    private Light flashingLight;
    private float originalIntensity;

    public float flashSpeed = 1.0f;
    public float minIntensity = 0.0f;
    public float maxIntensity = 1.0f;

    void Start()
    {
        // Get the Light component attached to this GameObject
        flashingLight = GetComponent<Light>();

        if (flashingLight == null)
        {
            Debug.LogError("No Light component found on the GameObject.");
            enabled = false; // Disable the script if no Light component is found
            return;
        }

        // Store the original intensity for later use
        originalIntensity = flashingLight.intensity;
    }

    void Update()
    {
        // Calculate the flash value using a sine function for a smooth oscillation
        float flashValue = Mathf.Sin(Time.time * flashSpeed);

        // Map the sine value to the desired intensity range
        float targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, (flashValue + 1) / 2);

        // Set the light intensity
        flashingLight.intensity = Mathf.Lerp(flashingLight.intensity, targetIntensity, Time.deltaTime * flashSpeed);
    }
}
