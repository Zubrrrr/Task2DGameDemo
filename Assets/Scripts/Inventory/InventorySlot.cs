using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public event Action<int> OnSlotClicked;

    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _itemCountText;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private int _slotIndex;

    private Item _item;
    private int _itemCount;
    private bool _deleteButtonVisible;
    private bool _useButtonVisible;

    public void SetItem(Item newItem, int count)
    {
        _item = newItem;
        _iconImage.sprite = _item.Icon;
        _iconImage.enabled = true;
        _itemCount = count;

        if (_itemCount > 1)
        {
            _itemCountText.text = _itemCount.ToString();
        }
        else
        {
            _itemCountText.text = "";
        }

        if (_item.isAmmo)
        {
            ShowUseButton();
        }
        else
        {
            HideUseButton();
        }

        HideDeleteButton();
    }

    public void ClearSlot()
    {
        _item = null;
        _iconImage.sprite = null;
        _iconImage.enabled = false;
        _itemCount = 0;
        _itemCountText.text = "";

        HideDeleteButton();
        HideUseButton();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_item != null)
        {
            if (_deleteButtonVisible)
            {
                HideDeleteButton();
            }
            else
            {
                ShowDeleteButton();
            }
        }

        OnSlotClicked?.Invoke(_slotIndex);
    }

    public void DeleteItem()
    {
        if (_item != null)
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.RemoveItem(_item);
            }
        }
    }

    public void UseItemAmmo()
    {
        if (_item != null && _item.isAmmo)
        {
            Shooting shooting = FindObjectOfType<Shooting>();
            shooting.ReloadAmmo(10);
        }
    }

    private void ShowDeleteButton()
    {
        _deleteButton.gameObject.SetActive(true);
        _deleteButtonVisible = true;
    }

    private void HideDeleteButton()
    {
        _deleteButton.gameObject.SetActive(false);
        _deleteButtonVisible = false;
    }

    private void ShowUseButton()
    {
        _useButton.gameObject.SetActive(true);
        _useButtonVisible = true;
    }

    private void HideUseButton()
    {
        _useButton.gameObject.SetActive(false);
        _useButtonVisible = false;
    }
}
