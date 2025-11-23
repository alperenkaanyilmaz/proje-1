using UnityEngine;

public class DoorController : Interactable
{
    public bool locked = false;
    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 3f;
    Quaternion closedRot;
    Quaternion openRot;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = transform.rotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        Quaternion target = isOpen ? openRot : closedRot;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * openSpeed);
    }

    public override void Interact(Transform interactor)
    {
        if (locked)
        {
            Debug.Log("Door is locked.");
            return;
        }
        isOpen = !isOpen;
    }

    public void Unlock()
    {
        locked = false;
    }
}
