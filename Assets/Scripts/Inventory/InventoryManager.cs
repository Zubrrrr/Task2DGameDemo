using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int _maxInventorySize = 10;
    [SerializeField] private Dictionary<Item, int> _inventoryItems = new Dictionary<Item, int>();
    [SerializeField] private Transform _inventorySlotsParent;
    [SerializeField] private GameObject _inventorySlotPrefab;

    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

    public void AddItem(Item item)
    {
        if (_inventoryItems.ContainsKey(item))
        {
            _inventoryItems[item]++;
        }
        else
        {
            _inventoryItems[item] = 1;
        }

        if (_inventoryItems.Count > _maxInventorySize)
        {
            Debug.Log("Инвентарь полон!");
            return;
        }

        UpdateInventoryUI();
    }

    public void RemoveItem(Item item)
    {
        if (_inventoryItems.ContainsKey(item))
        {
            _inventoryItems[item]--;
            if (_inventoryItems[item] <= 0)
            {
                _inventoryItems.Remove(item);
            }
        }

        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            Destroy(_inventorySlots[i].gameObject);
        }

        _inventorySlots.Clear();

        float slotWidth = _inventorySlotPrefab.GetComponent<RectTransform>().rect.width;
        float slotHeight = _inventorySlotPrefab.GetComponent<RectTransform>().rect.height;
        float spacing = 10f;

        float panelWidth = _inventorySlotsParent.GetComponent<RectTransform>().rect.width;
        int itemsPerRow = Mathf.FloorToInt(panelWidth / (slotWidth + spacing));

        float startXPos = -panelWidth / 2f + slotWidth / 2f;
        float startYPos = panelWidth / 2f - slotHeight / 2f;

        float currentXPos = startXPos;
        float currentYPos = startYPos;

        foreach (KeyValuePair<Item, int> kvp in _inventoryItems)
        {
            GameObject slotObject = Instantiate(_inventorySlotPrefab, _inventorySlotsParent);
            RectTransform slotTransform = slotObject.GetComponent<RectTransform>();
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();

            _inventorySlots.Add(slot);

            slotTransform.localPosition = new Vector3(currentXPos, currentYPos, 0f);

            currentXPos += slotWidth + spacing;

            if ((currentXPos - startXPos) >= panelWidth)
            {
                currentXPos = startXPos;
                currentYPos -= slotHeight + spacing;
            }

            slot.SetItem(kvp.Key, kvp.Value);
        }
    }

    private void Start()
    {
        CreateInventorySlots();
        UpdateInventoryUI();
    }

    private void CreateInventorySlots()
    {
        float slotWidth = _inventorySlotPrefab.GetComponent<RectTransform>().rect.width;
        float slotHeight = _inventorySlotPrefab.GetComponent<RectTransform>().rect.height;
        float spacing = 10f;

        float panelWidth = _inventorySlotsParent.GetComponent<RectTransform>().rect.width;
        int itemsPerRow = Mathf.FloorToInt(panelWidth / (slotWidth + spacing));

        float startXPos = -panelWidth / 2f + slotWidth / 2f;
        float startYPos = panelWidth / 2f - slotHeight / 2f;

        float currentXPos = startXPos;
        float currentYPos = startYPos;

        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            GameObject slotObject = Instantiate(_inventorySlotPrefab, _inventorySlotsParent);
            RectTransform slotTransform = slotObject.GetComponent<RectTransform>();
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            _inventorySlots.Add(slot);
            slot.OnSlotClicked += OnSlotClicked;
            slotTransform.localPosition = new Vector3(currentXPos, currentYPos, 0f);

            currentXPos += slotWidth + spacing;

            if ((i + 1) % itemsPerRow == 0)
            {
                currentXPos = startXPos;
                currentYPos -= slotHeight + spacing;
            }
        }
    }

    private void OnSlotClicked(int slotIndex)
    {

        // Метод нужен для правильной обработки события из класса inventorySlot

        // Также в будущем можно будет добавить различный код связанный например с описанием предмета и т д
    }
}
