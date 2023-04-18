using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int space = 4;
    public List<Item> items = new List<Item>();

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory.");
        }
        instance = this;
    }

    public bool Add(Item item)
    {
        if (!item.isDefault)
        {
            if (items.Count >= space)
            {
                Debug.Log("Inventory is full.");
                return false;
            }
            items.Add(item);
            if (OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }

        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
}
