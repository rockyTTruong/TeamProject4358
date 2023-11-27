using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    private QuickSlotManager qms;
    public DialogueTrigger defaultResponse;
    public TMP_Text SoldOut;
    public Button btn;
    
    void Start()
    {
        btn.interactable = true;
        SoldOut.gameObject.SetActive(false);
    }
    

    void Update()
    {
        
    }

    public void ClickBuy(Button button)
    {
        ItemData Item = button.gameObject.GetComponent<ShopItem>().item4Sale;
        qms = FindObjectOfType<QuickSlotManager>();
        if (canBuy(Item))
        {
            InventoryBox.Instance.AddItem(Item.id.ToString(),1);
            InventoryBox.Instance.RemoveItem("9999", Item.cost[0]);
            InventoryBox.Instance.RemoveItem("9998", Item.cost[1]);
            InventoryBox.Instance.RemoveItem("9997", Item.cost[2]);
            qms.UpdateUI();
            CoinManager.Instance.UpdateUI();
        }
        else
        {
            button.interactable = false;
            SoldOut.gameObject.SetActive(true);
            defaultResponse.triggerDialogue();

        }

    }


    

    private bool canBuy(ItemData Item)
    {
        int[] prices = Item.cost;

        if(Item.quantityToSell == 0)
        {
            return false;
        }
        
        if (prices[0] > InventoryBox.Instance.CheckInventory("9999").quantity)
        {
            return false;
        }
        if (prices[1] > InventoryBox.Instance.CheckInventory("9998").quantity)
        {
            return false;
        }
        if (prices[2] > InventoryBox.Instance.CheckInventory("9997").quantity)
        {
            return false;
        }
        return true;
        
    }
    
}
