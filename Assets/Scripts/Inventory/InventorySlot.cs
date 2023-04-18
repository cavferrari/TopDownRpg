using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Sprite clearSlotSprite;
    public Button discardButton;

    private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        discardButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = clearSlotSprite;
        icon.enabled = false;
        discardButton.interactable = false;
    }

    public void Use()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void OnEquipButton()
    {
        Debug.Log("Equiped");
    }

    public void OnBuyButton()
    {
        Debug.Log("Buying");
    }

    public void OnSellButton()
    {
        Debug.Log("Selling");
    }
}
