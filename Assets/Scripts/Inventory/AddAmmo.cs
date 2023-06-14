using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class AddAmmo : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        InventorySlot inventorySlot = GetComponentInParent<InventorySlot>();

        if (inventorySlot != null)
        {
            inventorySlot.UseItemAmmo();
            inventorySlot.DeleteItem();
        }
    }
}
