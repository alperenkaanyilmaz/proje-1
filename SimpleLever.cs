using UnityEngine;

public class SimpleLever : Interactable
{
    public bool isOn = false;
    public Transform pivot;
    public float toggleAngle = 40f;
    public float speed = 6f;
    public LeverPuzzle puzzleParent;

    Quaternion offRot;
    Quaternion onRot;

    void Start()
    {
        if (pivot == null) pivot = transform;
        offRot = pivot.localRotation;
        onRot = offRot * Quaternion.Euler(-toggleAngle, 0f, 0f);
    }

    void Update()
    {
        Quaternion target = isOn ? onRot : offRot;
        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, target, Time.deltaTime * speed);
    }

    public override void Interact(Transform interactor)
    {
        isOn = !isOn;
        if (puzzleParent != null)
        {
           
            foreach (var lv in puzzleParent.levers)
            {
                if (lv.leverTransform == this.transform)
                {
                    lv.isOn = this.isOn;
                    break;
                }
            }
            puzzleParent.CheckSolved();
        }
    }
}
