using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ShopManager : Manager<ShopManager>
{
    [SerializeField] int ingsPerShopContents;

    private ShopConfiguration currentShop;

    public void RefreshShop()
    {
        currentShop = new ShopConfiguration(ingsPerShopContents);
        UpdateDisplay();
    }
    
    private void UpdateDisplay()
    {
        // TODO: hook up with UI
    }

    public void InitializeShop()
    {
        RefreshShop();

        // TEMP: shop testing
        currentShop.DebugPrintConfig();

        RefreshShop();
        currentShop.DebugPrintConfig();
    }
}
