using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemDataSO ItemData { get; private set; }

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text quantityText;

    public void SetItem(ItemDataSO itemData, int quantity)
    {
        ItemData = itemData;

        itemImage.sprite = ItemData.itemSprite;
        itemImage.enabled = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
    }

    public void ClearSlot()
    {
        ItemData = null;
        itemImage.enabled = false;
        quantityText.enabled = false;
    }

    public void AddQuantity(int quantity)
    {
        if (ItemData != null)
        {
            int currentQuantity = int.Parse(quantityText.text);
            currentQuantity += quantity;
            quantityText.text = currentQuantity.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && ItemData != null)
        {
            InventoryManager.Instance.ShowItemDetails(ItemData);
        }
    }
}
