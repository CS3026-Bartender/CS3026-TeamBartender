using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CustomerState : MonoBehaviour
{
    protected TextMeshProUGUI DisplayText;


    public virtual void Enter() 
    {

    }
    public virtual void Exit() 
    {
        
    }

    public void Setup(TextMeshProUGUI _DisplayText) {
        DisplayText = _DisplayText;
    }
}
