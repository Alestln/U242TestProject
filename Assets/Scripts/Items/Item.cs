using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemDataSO itemData;

    [SerializeField]
    private int Quantity;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (itemData != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = itemData.itemSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

            if (inventoryManager != null)
            {
                inventoryManager.AddItem(itemData, Quantity);
            }

            Destroy(gameObject);
        }
    }
}
