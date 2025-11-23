using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{
    public Light lamp;
    public float maxBattery = 100f;
    public float drainPerSecond = 6f;
    public float idleDrainMultiplier = 0.2f; // when lamp on but dimmed
    public KeyCode toggleKey = KeyCode.F;
    public AudioClip toggleClip;
    AudioSource aud;
    public float currentBattery;

    [Header("Flicker")]
    public bool enableFlicker = false;
    public float flickerSpeed = 6f;
    public float flickerIntensity = 0.15f;

    void Start()
    {
        if (lamp == null) lamp = GetComponent<Light>();
        currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        aud = GetComponent<AudioSource>();
        if (aud == null) aud = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }

        if (lamp.enabled && currentBattery > 0f)
        {
            float drain = drainPerSecond * Time.deltaTime;
            currentBattery -= drain;
            if (currentBattery <= 0f)
            {
                currentBattery = 0f;
                lamp.enabled = false;
            }
        }

        if (enableFlicker && lamp.enabled)
        {
            lamp.intensity = 1f + Mathf.PerlinNoise(Time.time * flickerSpeed, 0f) * flickerIntensity;
        }
    }

    public void Toggle()
    {
        lamp.enabled = !lamp.enabled;
        if (toggleClip != null) aud.PlayOneShot(toggleClip);
    }

    public void Refill(float amount)
    {
        currentBattery = Mathf.Min(maxBattery, currentBattery + amount);
    }
}
