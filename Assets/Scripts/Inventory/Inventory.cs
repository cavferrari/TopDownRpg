using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

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
        OnItemChangedCallback.Invoke();
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
            OnItemChangedCallback.Invoke();
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
            OnItemChangedCallback.Invoke();
        }
        return true;
    }

    public void PlayerRemove(Item item)
    {
        playerItems.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public void ShopRemove(Item item)
    {
        shopItems.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
}
