using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Button _button;

    private bool _isInventoryOpen = false;

    private void Start()
    {
        _button.onClick.AddListener(ToggleInventory);
        _inventoryPanel.SetActive(false);
    }

    private void ToggleInventory()
    {
        _isInventoryOpen = !_isInventoryOpen;

        if (_isInventoryOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        _inventoryPanel.SetActive(true);
    }

    private void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
    }
}
