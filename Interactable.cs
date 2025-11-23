using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string prompt = "Interact";
    public virtual void Interact(Transform interactor)
    {
        Debug.Log($"{interactor.name} interacted with {name}");
    }
}
