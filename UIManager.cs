using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider batterySlider;
    public Slider healthSlider;
    Flashlight flashlight;
    PlayerDamage playerDamage;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            flashlight = player.GetComponentInChildren<Flashlight>();
            playerDamage = player.GetComponent<PlayerDamage>();
        }
    }

    void Update()
    {
        if (flashlight != null && batterySlider != null)
            batterySlider.value = flashlight.currentBattery / flashlight.maxBattery;
        if (playerDamage != null && healthSlider != null)
            healthSlider.value = playerDamage.currentHP / playerDamage.maxHP;
    }
}
