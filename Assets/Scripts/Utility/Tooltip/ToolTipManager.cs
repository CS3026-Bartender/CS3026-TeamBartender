using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;

public class ToolTipManager : Manager<ToolTipManager>
{
    public void Show(string content, string header = "") 
    {
        Tooltip.Instance.SetText(content, header);
        Tooltip.Instance.gameObject.SetActive(true);
    }

    public void Hide()
    {
        Tooltip.Instance.gameObject.SetActive(false);
    }

}
