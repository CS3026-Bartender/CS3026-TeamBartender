using UnityEngine;

public class CustomerOrderingState : CustomerState
{  
    public override void Enter() {
        Debug.Log("CustomerState: Ordering");
    }
}
