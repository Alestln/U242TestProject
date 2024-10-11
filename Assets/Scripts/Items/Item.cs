using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string name;

    [SerializeField]
    private InventoryManager inventoryManager;

    public string Name => name;

    public int Quantity { get; set; }

    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer is not null)
        {
            name = spriteRenderer.sprite.name;
        }

        Quantity = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();

        if (player is not null)
        {
            inventoryManager.AddItem(this);
            Destroy(gameObject);
        }
    }
}
