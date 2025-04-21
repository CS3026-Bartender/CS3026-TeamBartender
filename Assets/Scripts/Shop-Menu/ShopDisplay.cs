using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    public void UpdateDisplay(ShopConfiguration shopData)
    {
        for (int i = 0; i < 3; i++)
        {
            string ingID = shopData.GetIngID(i);
            if (DebugLogger.Instance.logShopDisplay) Debug.Log("Shop displaying " + ingID);
            GameObject slotObj = transform.GetChild(i).gameObject;
            slotObj.transform.GetChild(0).gameObject.SetActive(ingID != null);
            Slot slot = slotObj.GetComponent<Slot>();
            slot.UpdateSlot(ingID);
        }
    }
}
