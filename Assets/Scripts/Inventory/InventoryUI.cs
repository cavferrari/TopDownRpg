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
    private PlayerCustomize playerCustomize;

    void Awake()
    {
        Inventory.instance.OnItemChangedCallback += UpdateUI;
        playerInventorySlots = playerItemsParent.GetComponentsInChildren<InventorySlot>();
        shopInventorySlots = shopItemsParent.GetComponentsInChildren<InventorySlot>();
        playerCustomize = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCustomize>();
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
            shopInventoryUI.SetActive(!shopInventoryUI.activeSelf);
            if (!playerInventoryUI.activeSelf && shopInventoryUI.activeSelf)
            {
                playerInventoryUI.SetActive(true);
            }
            else if (!shopInventoryUI.activeSelf)
            {
                playerInventoryUI.SetActive(false);
            }
            EnablePlayerSlotsSellButton(shopInventoryUI.activeSelf);

        }
        if (!playerController.IsPlayerContactShopKeeper() && shopInventoryUI.activeSelf)
        {
            playerInventoryUI.SetActive(false);
            shopInventoryUI.SetActive(false);
            EnablePlayerSlotsSellButton(false);
        }
    }

    public void UpdateUI(Inventory.Action action)
    {
        if (action == Inventory.Action.Buying)
        {
            for (int i = 0; i < playerInventorySlots.Length; i++)
            {
                if (i < Inventory.instance.playerItems.Count)
                {
                    playerInventorySlots[i].AddItem(Inventory.instance.playerItems[i]);
                    playerInventorySlots[i].EnableSellButton(true);
                    playerInventorySlots[i].EnableEquipButton(true);
                }
                else
                {
                    /* 
                    * Needs to call the sell button disable before cleaning the slot becouse
                    * the enable function verify if the item is null before disabling the button. 
                    */
                    playerInventorySlots[i].EnableSellButton(false);
                    playerInventorySlots[i].EnableEquipButton(false);
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
            UpdateEquipButton();
        }
        else
        {
            UpdateEquipButton();
        }
    }

    public void EnablePlayerSlotsSellButton(bool enabled)
    {
        for (int i = 0; i < playerInventorySlots.Length; i++)
        {
            playerInventorySlots[i].EnableSellButton(enabled);
        }
    }

    public void UpdateEquipButton()
    {
        bool equiped;
        bool hasEquiped = false;
        for (int i = 0; i < playerInventorySlots.Length; i++)
        {
            equiped = (playerInventorySlots[i].GetItem() != null) ? playerInventorySlots[i].GetItem().equiped : false;
            if (equiped)
            {
                playerCustomize.UpdateEquipedSkin(playerInventorySlots[i].GetItem().name);
                hasEquiped = true;
            }
            playerInventorySlots[i].EnableEquipText(equiped);
        }
        if (!hasEquiped)
        {
            playerCustomize.UpdateEquipedSkin("Main");
        }
    }
}
