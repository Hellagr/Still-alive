using System.Collections.Generic;
using UnityEngine;

public class PlayerModifications : MonoBehaviour
{
    List<IPlayerModifiers> playerMovementModification = new List<IPlayerModifiers>();
    //List<IPlayerModifiers> damageModification = new List<IPlayerModifiers>();
    PlayerMovement playerMovement;
    float baseSpeed;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        baseSpeed = playerMovement.GetSpeed();
    }

    public void ApplyMovementModifier(IPlayerModifiers movementModifier)
    {
        if (movementModifier == null)
        {
            return;
        }
        playerMovementModification.Add(movementModifier);
        UpdateMovementSpeed();
    }

    public void RemoveMovementModifier(IPlayerModifiers movementModifier)
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
        foreach (IPlayerModifiers modifier in playerMovementModification)
        {
            newSpeed *= modifier.GetMovementModifier();
        }
        playerMovement.SetSpeed(newSpeed);
    }
}
