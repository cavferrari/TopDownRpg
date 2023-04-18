using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public enum Type { Player, Shop }

    public Type type = Type.Player;
    public Image icon;
    public Button itemButton;
    public Button equipButton;
    public Button sellButton;

    private Item item;

    public Item GetItem()
    {
        return item;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        itemButton.interactable = true;
        if (type == Type.Player && !item.equiped)
        {
            equipButton.interactable = true;
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        itemButton.interactable = false;
        if (type == Type.Player)
        {
            equipButton.interactable = false;
        }
    }

    public void EnableSellButton(bool enabled)
    {
        if (type == Type.Player && item != null)
        {
            sellButton.interactable = enabled;
        }
    }

    public void EnableEquipButton(bool enabled)
    {
        if (type == Type.Player && item != null)
        {
            equipButton.interactable = enabled;
        }
    }

    public void OnEquipButton()
    {
        Inventory.instance.Equip(item);
    }

    public void OnBuyButton()
    {
        Inventory.instance.PlayerAdd(item);
        Inventory.instance.ShopRemove(item);
    }

    public void OnSellButton()
    {
        Inventory.instance.ShopAdd(item);
        Inventory.instance.PlayerRemove(item);
    }
}
