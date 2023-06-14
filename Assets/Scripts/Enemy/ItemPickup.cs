using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] string _tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag))
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

            if (_item != null)
            {
                if (inventoryManager != null)
                {
                    inventoryManager.AddItem(_item);
                    Destroy(gameObject);
                }
            }
        }
    }
}
