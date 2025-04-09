using UnityEngine;

public class CustomerPayingState : CustomerState
{  
    public override void Enter() {
        Debug.Log("CustomerState: Paying");
    }
}
