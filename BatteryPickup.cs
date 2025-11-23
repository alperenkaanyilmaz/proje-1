using UnityEngine;

public class BatteryPickup : Interactable
{
    public float amount = 35f;
    public AudioClip pickupSound;

    public override void Interact(Transform interactor)
    {
        var flashlight = interactor.GetComponentInChildren<Flashlight>();
        if (flashlight != null)
        {
            flashlight.Refill(amount);
            if (pickupSound != null) AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}
