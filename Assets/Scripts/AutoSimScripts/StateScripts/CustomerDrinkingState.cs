using UnityEngine;

public class CustomerDrinkingState : CustomerState
{
    public override void Enter() {
        Debug.Log("CustomerState: Drinking");
    }
}
