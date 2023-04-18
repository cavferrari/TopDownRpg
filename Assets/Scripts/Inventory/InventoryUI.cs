using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform playerItemsParent;
    public Transform shopItemsParent;
    public GameObject playerInventoryUI;
    public GameObject shopInventoryUI;

    private InventorySlot[] playerInventorySlots;
    private InventorySlot[] shopInventorySlots;
    private PlayerController playerController;

    void Awake()
    {
        Inventory.instance.OnItemChangedCallback += UpdateUI;
        playerInventorySlots = playerItemsParent.GetComponentsInChildren<InventorySlot>();
        shopInventorySlots = shopItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        EnablePlayerSlotsSellButton(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !playerController.IsPlayerContactShopKeeper())
        {
            playerInventoryUI.SetActive(!playerInventoryUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Return) && playerController.IsPlayerContactShopKeeper())
        {
            playerInventoryUI.SetActive(!playerInventoryUI.activeSelf);
            shopInventoryUI.SetActive(!shopInventoryUI.activeSelf);
            EnablePlayerSlotsSellButton(shopInventoryUI.activeSelf);

        }
        if (!playerController.IsPlayerContactShopKeeper() && shopInventoryUI.activeSelf)
        {
            playerInventoryUI.SetActive(false);
            shopInventoryUI.SetActive(false);
            EnablePlayerSlotsSellButton(false);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < playerInventorySlots.Length; i++)
        {
            if (i < Inventory.instance.playerItems.Count)
            {
                playerInventorySlots[i].AddItem(Inventory.instance.playerItems[i]);
                playerInventorySlots[i].EnableSellButton(true);
            }
            else
            {
                /* 
                * Needs to call the sell button disable before cleaning the slot becouse
                * the enable function verify if the item is null before disabling the button. 
                */
                playerInventorySlots[i].EnableSellButton(false);
                playerInventorySlots[i].ClearSlot();
            }
        }
        for (int i = 0; i < shopInventorySlots.Length; i++)
        {
            if (i < Inventory.instance.shopItems.Count)
            {
                shopInventorySlots[i].AddItem(Inventory.instance.shopItems[i]);
            }
            else
            {
                shopInventorySlots[i].ClearSlot();
            }
        }
    }

    public void EnablePlayerSlotsSellButton(bool enabled)
    {
        for (int i = 0; i < playerInventorySlots.Length; i++)
        {
            playerInventorySlots[i].EnableSellButton(enabled);
        }
    }
}
