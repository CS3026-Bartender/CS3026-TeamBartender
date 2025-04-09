using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;

public class ToolTipManager : Manager<ToolTipManager>
{
    public Tooltip tooltip;

    public void Show(string content, string header = "") 
    {
        tooltip.SetText(content, header);
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }

}
