using UnityEngine;
using UnityEngine.EventSystems;

public class TestAddItem : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;

    public void AddItemButtonTest(Item item)
    {
        _inventoryManager.AddItem(item);
    }
}
