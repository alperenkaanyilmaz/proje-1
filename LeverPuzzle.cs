
using UnityEngine;
using System.Collections.Generic;

public class LeverPuzzle : Interactable
{
    [Header("Puzzle")]
    public List<Lever> levers;
    public bool solved = false;
    public GameObject doorToOpen; 

    public override void Interact(Transform interactor)
    {
        
    }

    public void CheckSolved()
    {
        if (solved) return;
        foreach (var l in levers)
        {
            if (!l.isOn) return;
        }
        solved = true;
        OnSolved();
    }

    void OnSolved()
    {
        Debug.Log("Puzzle solved!");
        if (doorToOpen != null)
        {
            var door = doorToOpen.GetComponent<DoorController>();
            if (door != null) door.Unlock();
        }
    }
}

[System.Serializable]
public class Lever
{
    public Transform leverTransform;
    public bool isOn = false;
}
