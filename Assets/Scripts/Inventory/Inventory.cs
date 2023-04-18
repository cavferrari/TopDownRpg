using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public enum Action { Buying, Equiping }

    public int space = 4;
    public List<Item> playerItems = new List<Item>();
    public List<Item> shopItems = new List<Item>();

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory.");
        }
        instance = this;
    }

    void Start()
    {
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke(Action.Buying);
        }
    }

    public bool PlayerAdd(Item item)
    {
        if (playerItems.Count >= space)
        {
            Debug.Log("PLayer Inventory is full.");
            return false;
        }
        playerItems.Add(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke(Action.Buying);
        }
        return true;
    }

    public bool ShopAdd(Item item)
    {
        if (shopItems.Count >= space)
        {
            Debug.Log("Shop Inventory is full.");
            return false;
        }
        shopItems.Add(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke(Action.Buying);
        }
        return true;
    }

    public void PlayerRemove(Item item)
    {
        playerItems.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke(Action.Buying);
        }
    }

    public void ShopRemove(Item item)
    {
        shopItems.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke(Action.Buying);
        }
    }

    public void Equip(Item item)
    {
        Item currentEquipedItem = null;
        for (int i = 0; i < playerItems.Count; i++)
        {
            if (playerItems[i].equiped)
            {
                currentEquipedItem = playerItems[i];
            }
        }
        if (currentEquipedItem != null)
        {
            if (currentEquipedItem.name.Equals(item.name))
            {
                currentEquipedItem.equiped = false;
            }
            else
            {
                currentEquipedItem.equiped = false;
                item.equiped = true;
            }
        }
        else
        {
            item.equiped = true;
        }
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke(Action.Equiping);
        }
    }

    public delegate void OnItemChanged(Action action);
    public OnItemChanged OnItemChangedCallback;
}
