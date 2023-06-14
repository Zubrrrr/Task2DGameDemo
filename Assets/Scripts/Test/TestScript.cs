using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private InventoryManager _inventoryManager;

    public void test(Item item)
    {
        _inventoryManager.AddItem(item);
        _inventoryManager.UpdateInventoryUI();
       
    }
}
