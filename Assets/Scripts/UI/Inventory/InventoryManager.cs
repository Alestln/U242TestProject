using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    private GameObject inventoryPanel;

    private bool menuActivated;

    [SerializeField]
    private ItemSlot slotPrefab;

    [SerializeField]
    private RectTransform slotsParent;

    [SerializeField]
    private ItemDetailsPanel itemDetailsPanel;

    [SerializeField]
    private int slotCount;

    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Start()
    {
        inventoryPanel.SetActive(menuActivated);
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        for (var i = 0; i < slotCount; i++)
        {
            ItemSlot newSlot = Instantiate(slotPrefab, slotsParent);
            newSlot.ClearSlot();
            itemSlots.Add(newSlot);
        }
    }

    public void OnOpen(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            menuActivated = !menuActivated;
            inventoryPanel.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1;
        }
    }

    public void AddItem(ItemDataSO itemData, int quantity)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.ItemData != null && slot.ItemData == itemData)
            {
                slot.AddQuantity(quantity);
                return;
            }
        }

        foreach (var slot in itemSlots)
        {
            if (slot.ItemData == null)
            {
                slot.SetItem(itemData, quantity);
                return;
            }
        }

        Debug.Log("Нет доступных слотов для добавления предмета: " + itemData.itemName);
    }

    public void ShowItemDetails(ItemDataSO itemData)
    {
        itemDetailsPanel.DisplayItemDetails(itemData);
    }

    public void ClearItemDetails()
    {
        itemDetailsPanel.ClearDetails();
    }
}
