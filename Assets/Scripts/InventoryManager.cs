using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> itemList = new();
    public EquipableItem weapon;
    public EquipableItem armor;

    public Transform inventoryContent;
    public GameObject inventoryItemTemplate;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item collectableItem)
    {
        itemList.Add(collectableItem);
    }

    public void Remove(Item collectableItem)
    {
        itemList.Remove(collectableItem);
    }

    public void Equip(EquipableItem collectableItem)
    {
        switch (collectableItem.type)
        {
            case EquipableType.Armor: 
                armor = collectableItem;
                break;
            case EquipableType.Weapon: 
                weapon = collectableItem;
                break;
        }            
        Remove(collectableItem);
    }

    public void Unequip(EquipableItem collectableItem)
    {
        switch (collectableItem.type)
        {
            case EquipableType.Armor: 
                armor = null;
                break;
            case EquipableType.Weapon: 
                weapon = null;
                break;
        }
        Add(collectableItem);
    }

    public void Drop(Item collectableItem)
    {}

    public void ListItems()
    {
        // clean inventory before opening
        // othewise the items multiply
        foreach (Transform collectableItem in inventoryContent)
        {
            Destroy(collectableItem.gameObject);
        }

        foreach (var collectableItem in itemList)
        {
            GameObject obj = Instantiate(inventoryItemTemplate, inventoryContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = collectableItem.icon; 
        }
    }

    public void Consume(ConsumableItem collectableItem)
    {
        // Player.heal(collectableItem.heals)
        Remove(collectableItem);
    }

}