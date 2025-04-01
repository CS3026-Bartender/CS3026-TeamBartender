using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    public void UpdateDisplay(ShopConfiguration shopData)
    {
        for (int i = 0; i < 3; i++)
        {
            string ingID = shopData.GetIngID(i);
            GameObject slotObj = transform.GetChild(i).gameObject;
            Slot slot = slotObj.GetComponent<Slot>();
            slot.UpdateSlot(ingID);
        }
    }
}
