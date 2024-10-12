using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField]
    private Image ItemImage;

    [SerializeField]
    private TMP_Text ItemNameText;

    [SerializeField]
    private TMP_Text ItemDescriptionText;

    public void DisplayItemDetails(ItemDataSO itemData)
    {
        ItemImage.sprite = itemData.itemSprite;
        ItemNameText.text = itemData.name;
        ItemDescriptionText.text = itemData.itemDescription;
    }

    public void ClearDetails()
    {
        ItemImage.sprite = null;
        ItemNameText.text = "";
        ItemDescriptionText.text = "";
    }
}
