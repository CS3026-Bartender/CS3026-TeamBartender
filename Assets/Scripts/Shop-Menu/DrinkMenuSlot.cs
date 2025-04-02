using UnityEngine;

public class DrinkMenuSlot : Slot
{
    private enum DrinkMenuState
    {
        Neutral,
        Available,
        Unavailable
    }

    private DrinkMenuState currentState;

    public void Activate(bool availability)
    {
        currentState = availability ? DrinkMenuState.Available : DrinkMenuState.Unavailable;
        dropArea.IsDropAllowed = availability;

        // TODO: set visual indication
    }

    public void Deactivate()
    {
        currentState = DrinkMenuState.Neutral;
        dropArea.IsDropAllowed = false;

        // TODO: remove visual indication (set back to neutral)
    }

    public override void UpdateSlot(string newID)
    {
        base.UpdateSlot(newID);
        if (ingDisplay != null)
        {
            // ingDisplay.SetDraggable(false);
        }
    }
}

