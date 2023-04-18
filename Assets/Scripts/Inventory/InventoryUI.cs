using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject playerInventoryUI;
    public GameObject shopInventoryUI;

    private InventorySlot[] inventorySlots;
    private PlayerController playerController;

    void Start()
    {
        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        Inventory.instance.OnItemChangedCallback += UpdateUI;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerInventoryUI.SetActive(!playerInventoryUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Return) && playerController.IsPlayerContactShopKeeper())
        {
            shopInventoryUI.SetActive(!shopInventoryUI.activeSelf);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
            {
                inventorySlots[i].AddItem(Inventory.instance.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
