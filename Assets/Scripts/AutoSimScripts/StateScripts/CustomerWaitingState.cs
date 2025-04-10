using UnityEngine;

public class CustomerWaitingState : CustomerState
{
    
    public override void Enter() {
        Debug.Log("CustomerState: Waiting");
    }
}
