using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsPanel;

    private bool menuActivated;

    [SerializeField]
    private ItemSlot slotPrefab;

    private Dictionary<string, ItemSlot> inventorySlots = new Dictionary<string, ItemSlot>();

    private void Start()
    {
        inventoryPanel.SetActive(menuActivated);
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

    public void AddItem(Item item)
    {
        if (inventorySlots.TryGetValue(item.Name, out ItemSlot existingSlot))
        {
            existingSlot.AddQuantity(item.Quantity);
        }
        else
        {
            ItemSlot newSlot = Instantiate(slotPrefab, inventorySlotsPanel.transform);
            newSlot.AddItem(item);
            inventorySlots[item.Name] = newSlot;
        }
    }
}
