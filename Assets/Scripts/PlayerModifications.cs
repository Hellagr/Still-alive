using System.Collections.Generic;
using UnityEngine;

public class PlayerModifications : MonoBehaviour
{
    List<IDataModifier> playerMovementModification = new List<IDataModifier>();
    PlayerMovement playerMovement;
    float baseSpeed;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        baseSpeed = playerMovement.GetSpeed();
    }

    public void ApplyMovementModifier(IDataModifier movementModifier)
    {
        if (movementModifier == null)
        {
            return;
        }
        playerMovementModification.Add(movementModifier);
        UpdateMovementSpeed();
    }

    public void RemoveMovementModifier(IDataModifier movementModifier)
    {
        if (movementModifier == null)
        {
            return;
        }
        playerMovementModification.Remove(movementModifier);
        UpdateMovementSpeed();
    }

    void UpdateMovementSpeed()
    {
        float newSpeed = baseSpeed;
        foreach (IDataModifier modifier in playerMovementModification)
        {
            newSpeed *= modifier.GetMovementModifier();
        }
        playerMovement.SetSpeed(newSpeed);
    }
}
