using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite Icon;
    public bool isAmmo;
}