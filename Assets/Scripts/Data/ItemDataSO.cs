using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public int maxQuantity;
}
