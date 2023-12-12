using UnityEngine;

public class LightIntensityController : MonoBehaviour
{
    public float minRange = 0.1f;
    public float maxRange = 1f;
    public float minIntensity = 0.3f;
    public float maxIntensity = 2f;
    public float changeSpeed = 1f;

    private Light pointLight;
    private float timeOffset;

    void Start()
    {
        pointLight = GetComponent<Light>();
        if (pointLight == null)
        {
            Debug.LogError("LightIntensityController requires a Light component on the GameObject.");
            enabled = false; // Disable the script if the Light component is not found.
        }

        // Store the initial time for the offset
        timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // Change range and intensity over time using Mathf.Sin
        float t = Mathf.Sin((Time.time + timeOffset) * changeSpeed);
        float newRange = Mathf.Lerp(minRange, maxRange, (t + 1f) / 2f);
        float newIntensity = Mathf.Lerp(minIntensity, maxIntensity, (t + 1f) / 2f);

        pointLight.range = newRange;
        pointLight.intensity = newIntensity;
    }
}
